using Reservation.Data.Model.Amenities;
using Reservation.Data.Model.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Model.RoomAmenities
{
    public class RoomAmenity
    {
        public int RoomAmenityId { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; } 

        public int AmenityId { get; set; }
        public Amenity Amenity { get; set; }
    }
}
