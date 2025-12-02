using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Dtos.ImageDto
{
    public class AddRoomType
    {
        public string Name { get; set; }
        public double BasePrice { get; set; }
        public string BedType { get; set; }
        public int MaxOccupancy { get; set; }
    }
}
