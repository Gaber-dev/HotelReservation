using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Reservation.API.Services.Auth;
using Reservation.API.Services.Email;
using Reservation.Domain.Dtos.AccountDto;

namespace Reservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly IEmailService _emailservice;

        public AccountController(IAuthService service , IEmailService emailservice)
        {
           _service = service;
           _emailservice = emailservice;
        }

        [HttpPost("Register-Client")]
        public async Task<IActionResult> RegisterClient([FromBody] RegisterDto datafromclient)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result =  await _service.RegisterClientAsync(datafromclient);
            if (result == "تم إنشاء الحساب بنجاح")
                return Ok(new { message = result });

            return BadRequest(new { message = result });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Register-Receptionist")]
        public async Task<IActionResult> RegisterReceptionist([FromBody] RegisterDto datafromrec)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result =  await _service.RegisterReceptionist(datafromrec);
            if (result == "تم إنشاء الحساب بنجاح")
                return Ok(new { message = result });

            return BadRequest(new { message = result });
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.Login(dto);

            if (result == null)
                return BadRequest(new { message = "البريد الإلكتروني أو كلمة المرور غير صحيحة" });

            return Ok(result);
        }


        
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.Forgotpassword(dto);
            if (result == "This email is not found please sign up to continue")
            {
                return NotFound(new { message = result });
            }

            return Ok(new { message = result });
        }

        
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto rdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.ResetPassword(rdto);
            if (result == "Invalid Email" || result == "Invalid or expired code.")
            {
                return BadRequest(new { message = result });
            }
            if (result.Contains("|"))
            {
                return BadRequest(new { message = "Password reset failed", errors = result });
            }

            return Ok(new { message = result });
        }



    }
}

