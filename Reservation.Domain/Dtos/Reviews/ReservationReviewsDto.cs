using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Dtos.Reviews
{
    public class ReservationReviewsDto
    {
        public string GuestName { get; set; }
        public double? Rating { get; set; }           
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
