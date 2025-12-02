using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reservation.Domain.Dtos.ImageDto;
using Reservation.Domain.Managers.RoomTypeManager;

namespace Reservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypeController : ControllerBase
    {
        private readonly IRoomTypeService _service;

        public RoomTypeController(IRoomTypeService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddRoomType(AddRoomType dto)
        {
            await _service.Add(dto);
            return Ok();
        }
    }
}
