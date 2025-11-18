using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Dtos.RoomsDto
{
    public class RoomDisplayDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RoomTypeName { get; set; }

        public double PricePerNight { get; set; }

        public double Rating {  get; set; }
        public int ReviewCount { get; set; }

        public string BedType { get; set; }

        public int MaxOccupancy { get; set; }
        public string Size { get; set; }
        public string MainImageUrl { get; set; }

        public List<String> Amenities { get; set; }
    }
}
