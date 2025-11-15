using Reservation.Domain.Interfaces;
using Reservation.Domain.Models;

namespace Reservation.Infrastructure.Services
{
    public class HotelService : IHotelService
    {
        public HeroSection GetHeroSection() => new HeroSection
        {
            Title = "Experience Luxury & Comfort",
            Subtitle = "Discover your perfect escape in paradise",
            ButtonText = "Explore Our Rooms",
            ButtonLink = "/rooms",
            ImageUrl = "https://example.com/images/hero.jpg"
        };

        public IEnumerable<Feature> GetFeatures() => new List<Feature>
        {
            new Feature { Icon = "wifi", Title = "Free WiFi", Description = "High-speed internet throughout the property" },
            new Feature { Icon = "utensils", Title = "Fine Dining", Description = "Award-winning restaurant and bar" },
            new Feature { Icon = "concierge", Title = "Concierge", Description = "24/7 dedicated concierge service" }
        };

        public IEnumerable<Testimonial> GetTestimonials() => new List<Testimonial>
        {
            new Testimonial { Author = "Sarah Johnson", Location = "New York", Comment = "The most amazing hotel experience. The ocean view room was breathtaking!", Rating = 5 },
            new Testimonial { Author = "Michael Chen", Location = "Singapore", Comment = "Exceptional service and stunning facilities. Will definitely return!", Rating = 5 },
            new Testimonial { Author = "Emma Williams", Location = "London", Comment = "Perfect location, luxurious rooms, and wonderful staff. Highly recommend!", Rating = 5 }
        };

        public FooterInfo GetFooterInfo() => new FooterInfo
        {
            HotelName = "Grand Azure Hotel",
            Description = "Experience luxury and comfort in paradise. Your perfect escape awaits at Grand Azure Hotel.",
            Address = "123 Ocean Drive, Paradise Bay, Tropical Island, 13024",
            Phone = "+1 (234) 567-8900",
            Email = "info@grandazure.com",
            QuickLinks = new List<string> { "Home", "Rooms", "About Us", "Contact", "Terms & Conditions", "Privacy Policy" },
            SocialLinks = new List<string> { "Facebook", "Instagram", "Twitter" }
        };
    }
}
