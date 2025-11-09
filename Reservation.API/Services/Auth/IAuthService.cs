using Reservation.Domain.Dtos.AccountDto;

namespace Reservation.API.Services.Auth
{
    public interface IAuthService
    {
        Task<string> RegisterClientAsync(RegisterDto clientdto);

        Task<string> RegisterReceptionist(RegisterDto ReceptionistDto);
    }
}
