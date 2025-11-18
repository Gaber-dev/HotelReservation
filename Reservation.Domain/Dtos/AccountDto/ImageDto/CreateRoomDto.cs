using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Dtos.ImageDto
{
    public class CreateRoomDto
    {
        public int RoomNumber { get; set; }
        public string RoomName { get; set; }
        public int FloorNumber { get; set; }
        public string RoomTypeName { get; set; }

        public double PricePerRoom { get; set; }
        public int Capacity { get; set; }

        public string Status { get; set; }

        public string RoomSize { get; set; }

        public string BedType { get; set; }

        public string View { get; set; }
        public string Smooking { get; set; }
        public string Description { get; set; }
        public List<string> Amenities { get; set; }

        public List<string> Images { get; set; }

    }
}
