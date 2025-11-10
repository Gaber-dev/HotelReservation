using Reservation.Data.Model.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Model.RoomImages
{
    public class RoomImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        public int RoomId { get; set; }
        public Room room { get; set; }


    }
}
