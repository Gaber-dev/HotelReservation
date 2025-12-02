using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Dtos.Pricesdto
{
    public class PriceRequestCalculateDto
    {
        public int RoomId { get; set; }
        public DateTime CheckInDate{ get; set; }
        public DateTime CheckOutDate { get; set; }
        public int Guests { get; set; }
    }
}
