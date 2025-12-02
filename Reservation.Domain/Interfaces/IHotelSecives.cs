using Reservation.Domain.Models;

namespace Reservation.Domain.Interfaces
{
    public interface IHotelService
    {
        HeroSection GetHeroSection();
        IEnumerable<Feature> GetFeatures();
        IEnumerable<Testimonial> GetTestimonials();
        FooterInfo GetFooterInfo();
    }
}
