using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reservation.API.Managers.ReservationManager;
using Reservation.Domain.Dtos.PaymobDto;

namespace Reservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public PaymentController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost("paymob-callback")]
        [AllowAnonymous]
        public async Task<IActionResult> PaymobCallback([FromBody] PaymobCallbackDto dto)
        {
            await _reservationService.HandlePaymobCallbackAsync(dto);
            return Ok();
        }
    }
}

