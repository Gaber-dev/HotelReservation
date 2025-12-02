using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Dtos.ReservationDto
{
    public class GetCurrentReservationsDto
    {
        public string ConfirmationCode { get; set; }
        public int roomNumber { get; set; }
        public string status { get; set; }
        public string View {  get; set; }

        public string HotelName { get; set; } = "Grand Azure Hotel";
        public DateTime checkindate { get; set; }
        public DateTime checkoutdate { get; set; }

        public double TotalAmount { get; set; }

    }
}
