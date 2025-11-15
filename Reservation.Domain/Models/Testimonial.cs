namespace Reservation.Domain.Models
{
    public class Testimonial
    {
        public string Author { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public int Rating { get; set; }
    }
}
