using Reservation.Domain.Dtos.PaymobDto;
using Reservation.Domain.Dtos.ReservationDto;

namespace Reservation.API.Managers.ReservationManager
{
    public interface IReservationService
    {
        Task<CreateReservationResponseDto> CreateReservationAndInitPaymentAsync(string userId, CreateReservationRequestDto dto);
        Task HandlePaymobCallbackAsync(PaymobCallbackDto callback);
    }
}
