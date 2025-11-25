using Reservation.Data.Model.Amenities;
using Reservation.Data.Model.Reservations;
using Reservation.Data.Model.RoomAmenities;
using Reservation.Data.Model.RoomImages;
using Reservation.Data.Model.Roomtype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Model.Rooms
{
    public class Room
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public double PricePerroom { get; set; }
        public int FloorNumber { get; set; }
        public string Size { get; set; }
        public string View { get; set; }
        public string Smooking { get; set; }
        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }
        public ICollection<RoomImage> RoomImages { get; set; } = new HashSet<RoomImage>();
        public ICollection<RoomAmenity> Amenities { get; set; } = new HashSet<RoomAmenity>();
        public ICollection<Reserve> Reservations { get; set; } = new HashSet<Reserve>();


    }
}
