using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Dtos.AccountDto
{
    public class UpdateAddressInformationsDto
    {
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public int? ZipCode { get; set; }
    }
}
