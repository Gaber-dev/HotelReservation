namespace Reservation.Domain.Models
{
    public class FooterInfo
    {
        public string HotelName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<string> QuickLinks { get; set; } = new();
        public List<string> SocialLinks { get; set; } = new();
    }
}
