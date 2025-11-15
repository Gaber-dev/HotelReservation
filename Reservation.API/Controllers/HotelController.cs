using Microsoft.AspNetCore.Mvc;
using Reservation.Domain.Interfaces;
using Reservation.Domain.Models;

namespace Reservation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet("hero")]
        public ActionResult<HeroSection> GetHero() => Ok(_hotelService.GetHeroSection());

        [HttpGet("features")]
        public ActionResult<IEnumerable<Feature>> GetFeatures() => Ok(_hotelService.GetFeatures());

        [HttpGet("testimonials")]
        public ActionResult<IEnumerable<Testimonial>> GetTestimonials() => Ok(_hotelService.GetTestimonials());

        [HttpGet("footer")]
        public ActionResult<FooterInfo> GetFooter() => Ok(_hotelService.GetFooterInfo());
    }
}
