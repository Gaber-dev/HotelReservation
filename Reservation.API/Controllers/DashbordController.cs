using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reservation.API.Managers.AccountManager;
using Reservation.Domain.Dtos.AccountDto;
using Reservation.Domain.Dtos.ReservationDto;
using System.Security.Claims;

namespace Reservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashbordController : ControllerBase
    {
        private readonly IDashbordService _service;

        public DashbordController(IDashbordService service)
        {
            _service = service;
        }



        [HttpGet("/Dashbord")]
        [Authorize]
        public async Task<DashbordDto> GetDashbordInformations(string userId)
        {
            var res =  await _service.GetDashbordDetails(userId);
            return res;
        }


        [HttpGet("/My Reservations")]
        [Authorize]
        public async Task<List<MyReservationsDto>> MyReservationsAll(string userId)
        {
            var res = await _service.MyReservations(userId);
            return res;
        }



        [HttpGet("/Profile Settings")]
        [Authorize]
        public async Task<ProfileSettingsDto> Profilesettings(string userId)
        {
            var res = await _service.ProfileSettings(userId);
            return res;
        }


        [Authorize]
        [HttpPut("profile-settings")]
        public async Task<IActionResult> UpdateProfileSettings([FromBody] UpdateProfileSettingsDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            await _service.UpdateProfileSettings(userId, dto);
            return NoContent(); 
        }
    }
}

