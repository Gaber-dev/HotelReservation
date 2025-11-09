using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Reservation.API.Services.Auth;
using Reservation.Domain.Dtos.AccountDto;

namespace Reservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _service;

        public AccountController(IAuthService service)
        {
           _service = service;
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


    }
}
