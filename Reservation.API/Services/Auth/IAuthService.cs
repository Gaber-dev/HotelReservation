using Microsoft.AspNetCore.Identity;
using Reservation.Domain.Dtos.AccountDto;

namespace Reservation.API.Services.Auth
{
    public interface IAuthService
    {
        Task<string> RegisterClientAsync(RegisterDto clientdto);
        Task<string> RegisterReceptionist(RegisterDto ReceptionistDto);
        Task<LoginResponseDto> Login(LoginDto userdate);
        Task<string> Forgotpassword(ForgotPasswordDto dto);
        Task<string> ResetPassword(ResetPasswordDto rdto);

    }
}
