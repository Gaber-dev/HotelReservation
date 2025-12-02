using Reservation.Domain.Dtos.AccountDto;
using Reservation.Domain.Dtos.ReservationDto;

namespace Reservation.API.Managers.AccountManager
{
    public interface IDashbordService
    {
        Task<DashbordDto> GetDashbordDetails(string userId);
        Task<List<MyReservationsDto>> MyReservations(string userId);
        Task<ProfileSettingsDto> ProfileSettings(string userId);
        Task<bool> UpdateProfileSettings(string userId, UpdateProfileSettingsDto dto);

    }
}
