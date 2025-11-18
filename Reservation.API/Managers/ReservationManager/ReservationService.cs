using Microsoft.EntityFrameworkCore;
using Reservation.API.Managers.Paymob;
using Reservation.Data.Data;
using Reservation.Data.Model.Guests;
using Reservation.Data.Model.Invoices;
using Reservation.Data.Model.Payments;
using Reservation.Data.Model.Reservations;
using Reservation.Domain.Dtos.PaymobDto;
using Reservation.Domain.Dtos.Pricesdto;
using Reservation.Domain.Dtos.ReservationDto;
using Reservation.Domain.Managers.RoomManager;

namespace Reservation.API.Managers.ReservationManager
{
    public class ReservationService  : IReservationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPaymobService _paymobService;
        private readonly IRoomService _roomService;
        private readonly IConfiguration _configuration; 

        public ReservationService(ApplicationDbContext context, IPaymobService paymobService, IRoomService roomService, IConfiguration configuration)
        {
            _context = context;
            _paymobService = paymobService;
            _roomService = roomService;
            _configuration = configuration;
            _configuration = configuration;
        }

        public async Task<CreateReservationResponseDto> CreateReservationAndInitPaymentAsync(string userId, CreateReservationRequestDto dto)
        {
            
            var price = await _roomService.CalculatePriceToRoom(new PriceRequestCalculateDto
            {
                RoomId = dto.RoomId,
                CheckInDate = dto.CheckinDate,
                CheckOutDate = dto.CheckoutDate,
                Guests = dto.GuestsCount
            });

            
            var reserve = new Reserve
            {
                CheckinDate = dto.CheckinDate,
                CheckoutDate = dto.CheckoutDate,
                status = "Pending",
                ConfirmationCode = Guid.NewGuid().ToString("N").Substring(0, 8),
                AppUserId = userId,
                RoomId = dto.RoomId
            };
            _context.Reservations.Add(reserve);
            await _context.SaveChangesAsync();

            var invoice = new Invoice
            {
                issuedate = DateTime.UtcNow,
                TotalAmount = price.TotalAmount,
                Discount = 0,
                TaxAmount = price.TaxesAndFees,
                NetAmount = price.TotalAmount,
                ReserveId = reserve.Id
            };
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            // payment
            var payment = new Payment
            {
                Name = "Card",
                AmountPaid = price.TotalAmount,
                Status = "Pending",
                InvoiceId = invoice.Id
            };
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();


            var (paymentToken, paymobOrderId) = await _paymobService.CreatePaymentKeyAsync(
     price.TotalAmount,
     "EGP",
     dto.Email,
     dto.FullName
 );

            // أضف السطر ده:
            payment.PaymobOrderId = paymobOrderId;
            await _context.SaveChangesAsync();


            var iframeId = _configuration["Paymob:IframeId"]
                  ?? throw new InvalidOperationException("Paymob:IframeId is missing in appsettings.json");

            var iframeUrl = $"https://accept.paymob.com/api/acceptance/iframes/{iframeId}?payment_token={paymentToken}";

            return new CreateReservationResponseDto
            {
                ReservationId = reserve.Id,
                InvoiceId = invoice.Id,
                PaymentId = payment.Id,
                PaymentIframeUrl = iframeUrl,
                ConfirmationCode = reserve.ConfirmationCode
            };
        }

        public async Task HandlePaymobCallbackAsync(PaymobCallbackDto callback)
        {
            var obj = callback.obj; // ← كل البيانات الحقيقية هنا

            // ابحث عن الدفع بالـ PaymobOrderId (اللي حفظناه لما عملنا الحجز)
            var payment = await _context.Payments
                .Include(p => p.Invoice)
                .ThenInclude(i => i!.Reserve)
                .FirstOrDefaultAsync(p => p.PaymobOrderId == obj.order.id.ToString());

            if (payment == null)
                return;

            if (obj.success && obj.is_paid)
            {
                payment.Status = "Paid";
                payment.PaymobOrderId = obj.id.ToString();
                payment.PaymentDate = DateTime.UtcNow;

                if (payment.Invoice?.Reserve != null)
                    payment.Invoice.Reserve.status = "Confirmed";
            }
            else
            {
                payment.Status = "Failed";
                if (payment.Invoice?.Reserve != null)
                    payment.Invoice.Reserve.status = "Cancelled";
            }

            await _context.SaveChangesAsync();
        }
    }

}
