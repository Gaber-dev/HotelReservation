using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Reservation.API.Managers.ReservationManager;
using Reservation.Data.Model.User;
using Reservation.Domain.Dtos.ReservationDto;

namespace Reservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly UserManager<AppUser> _userManager;

        public ReservationsController(IReservationService reservationService, UserManager<AppUser> userManager)
        {
            _reservationService = reservationService;
            _userManager = userManager;
        }

        [HttpPost("create")]
        [Authorize] // user must be logged in
        public async Task<ActionResult<CreateReservationResponseDto>> CreateReservation([FromBody] CreateReservationRequestDto dto)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _reservationService.CreateReservationAndInitPaymentAsync(userId!, dto);
            return Ok(result);   
        }
    }
}
