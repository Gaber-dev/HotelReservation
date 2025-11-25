using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Dtos.RoomsDto
{
    public class RoomSearchFilterDto
    {
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public int? NumberOfGuests { get; set; }

        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }

        public List<string>? RoomTypes { get; set; }

        public List<String>? Amenities { get; set; }
    }
}
