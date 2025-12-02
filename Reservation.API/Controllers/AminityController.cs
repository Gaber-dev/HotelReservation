using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reservation.Domain.Dtos.AminityDto;
using Reservation.Domain.Managers.AminityManager;

namespace Reservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AminityController : ControllerBase
    {
        private readonly IAminityService _service;

        public AminityController(IAminityService service)
        {
            _service = service;
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddAminity(AddAminityDto dto)
        {
            await _service.AddAminity(dto);
            return Ok();
        }
    }
}
