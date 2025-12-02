using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reservation.Domain.Dtos.ImageDto;
using Reservation.Domain.Dtos.Pricesdto;
using Reservation.Domain.Dtos.RoomsDto;
using Reservation.Domain.Managers.RoomManager;

namespace Reservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _service;

        public RoomController(IRoomService service)
        {
           _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddRoom([FromBody] CreateRoomDto dto)
        {
            await _service.AddRoom(dto);
            return Ok("Room created successfully");
        }



        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] RoomSearchFilterDto filter)
        {
            if (filter.CheckInDate.HasValue && filter.CheckOutDate.HasValue)
            {
                if (filter.CheckOutDate.Value.Date <= filter.CheckInDate.Value.Date)
                    return BadRequest("Check-out date must be after check-in date.");
            }

            if (filter.NumberOfGuests.HasValue && filter.NumberOfGuests <= 0)
                return BadRequest("Number of guests must be greater than 0.");
            var rooms = await _service.GetFilteredAvailableRooms(filter);
            return Ok(rooms); 
        }




        [HttpGet("/id")]
        public async Task<IActionResult> GetRoomDetails(int roomId)
        {
            try
            {
                var room = await _service.GetRoomDetailsWithReviewsAsync(roomId);
                return Ok(room);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Room not found");
            }
        }


        [HttpPost("calculate")]
        public async Task<IActionResult> Calculate([FromBody] PriceRequestCalculateDto dto)
        {
            var result = await _service.CalculatePriceToRoom(dto);
            return Ok(result);
        }






    }
}
