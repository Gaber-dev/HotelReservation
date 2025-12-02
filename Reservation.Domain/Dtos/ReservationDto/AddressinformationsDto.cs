using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Dtos.ReservationDto
{
    public class AddressinformationsDto
    {
        public string StreetAddress { get; set; } = string.Empty;
        public string City { get; set; }   = string.Empty;
        public string state { get; set; } = string.Empty;
        public string country { get; set; }   = string.Empty;
        public int ZipCode { get; set; } = 0;
    }
}
