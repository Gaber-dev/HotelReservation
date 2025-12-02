using Reservation.Data.Model.Reservations;
using Reservation.Data.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Model.ReservationReviews
{
    public class ReservationReview
    {
        public int Id { get; set; }
        public double? Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int ReserveId { get; set; }
        public Reserve Reserve { get; set; }

    }
}
