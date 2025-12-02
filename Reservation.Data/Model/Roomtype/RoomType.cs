using Reservation.Data.Model.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Model.Roomtype
{
    public class RoomType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double BasePrice { get; set; }
        public int MaxOccupancy { get; set; }
        public string BedType { get; set; }
        public ICollection<Room> rooms = new HashSet<Room>();
    }
}
