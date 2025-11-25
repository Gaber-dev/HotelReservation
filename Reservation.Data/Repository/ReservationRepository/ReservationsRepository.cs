using Microsoft.EntityFrameworkCore;
using Reservation.Data.Data;
using Reservation.Data.Model.Reservations;
using Reservation.Data.Repository.GenericRepository;
using Reservation.Domain.Dtos.ReservationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Repository.ReservationRepository
{
    public class ReservationsRepository :  GenericRepository<Reserve> , IReservationsRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservationsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> GetTotalReservatioCount(string userId)
        {
            var numberofreservations = await _context.Reservations.CountAsync(r => r.status == "Checked Out" && r.AppUserId == userId);
            return numberofreservations;
        }



        public async Task<double> GetTotalReservatioMoneyspent(string userId)
        {
            var totalamount = 0;
            var numberofreservations = await _context.Reservations.Where(r => r.status == "Checked Out" && r.AppUserId == userId).ToListAsync();
            foreach ( var item in numberofreservations)
            {
                totalamount += (int) item.Invoice.TotalAmount;
            }
            return totalamount;
        }


        public async Task<List<GetCurrentReservationsDto>> GetCurrentReservations(string userId)
        {
            var currentReservationsDtoList = await _context.Reservations.Where(r => r.status == "Confirmed" && r.AppUserId == userId).Select(r => new GetCurrentReservationsDto
        {
            ConfirmationCode = r.ConfirmationCode,
            status = r.status,
            checkindate = r.CheckinDate,
            checkoutdate = r.CheckoutDate,
            roomNumber = r.room.Number,
            TotalAmount = r.Invoice.TotalAmount,
            View = r.room.View
        }).ToListAsync();

            return currentReservationsDtoList;
        }



        public async Task<List<GetReservationHistoryDto>> GetReservationsHistory(string userId)
        {
            var ReservationsHistory = await _context.Reservations.Where(r => r.status == "Checked Out" && r.AppUserId == userId).Select(r => new GetReservationHistoryDto 
            {
                ConfirmationCode = r.ConfirmationCode,
                status = r.status,
                checkindate = r.CheckinDate,
                checkoutdate = r.CheckoutDate,
                roomNumber = r.room.Number,
                TotalAmount = r.Invoice.TotalAmount,
                View = r.room.View
            }).ToListAsync();

            return ReservationsHistory;
        }




        public async Task<List<MyReservationsDto>> MyReservationsAll(string userId)
        {
            var AllReservations = await _context.Reservations.Where(r => ( 
            r.status == "Checked Out" ||
            r.status == "Confirmed" ||
            r.status == "Cancelled" ||
            r.status == "Checked In" ) && r.AppUserId == userId ).Select(r => new MyReservationsDto
              {
                ConfirmationCode = r.ConfirmationCode,
                status = r.status,
                checkindate = r.CheckinDate,
                checkoutdate = r.CheckoutDate,
                roomNumber = r.room.Number,
                TotalAmount = r.Invoice.TotalAmount,
                View = r.room.View
               }).ToListAsync();

                return AllReservations;
        }




    }
}
