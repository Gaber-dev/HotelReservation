using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Dtos.ImageDto
{
    public class CreateRoomImageDto
    {
        public List<string> ImageURL { get; set; }
        public int RoomNumber { get; set; }
    }
}
