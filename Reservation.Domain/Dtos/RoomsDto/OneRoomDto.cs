using Reservation.Domain.Dtos.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Dtos.RoomsDto
{
    public class OneRoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double PricePerNight { get; set; }
        public string Size { get; set; }
        public string View { get; set; }
        public string BedType { get; set; }
        public int MaxOccupancy { get; set; }
        public string Smoking { get; set; } = "Non-Smoking";

        public double? Rating { get; set; }
        public int ReviewCount { get; set; }

        public List<string> Images { get; set; }
        public List<string> Amenities { get; set; }

        public List<ReservationReviewsDto> Reviews { get; set; } = new();
    }
}
