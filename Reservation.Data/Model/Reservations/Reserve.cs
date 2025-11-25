using Reservation.Data.Model.Guests;
using Reservation.Data.Model.Invoices;
using Reservation.Data.Model.ReservationReviews;
using Reservation.Data.Model.Rooms;
using Reservation.Data.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Model.Reservations
{
    public class Reserve
    {
        public int Id { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime CheckoutDate { get; set; }
        public string status { get; set; }
        public string ConfirmationCode { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string AppUserId { get; set; }
        public AppUser ClientId { get; set; }
        public int RoomId { get; set; }
        public Room room { get; set; }
        public int? GuestId { get; set; }
        public Guest? Guest { get; set; }    
        public Invoice Invoice { get; set; }
        public ReservationReview ReservationReview { get; set; }
    }
}
