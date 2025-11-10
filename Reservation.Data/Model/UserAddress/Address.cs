using Reservation.Data.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Model.UserAddress
{
    public class Address
    {
        public int Id { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public int? ZipCode { get; set; }

        public string AppUserId { get; set; }
        public AppUser appUser { get; set; }
    }
}
