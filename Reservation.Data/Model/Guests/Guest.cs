using Reservation.Data.Model.Reservations;
using Reservation.Data.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Model.Guests
{
    public class Guest
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }

        public DateTime createdAt { get; set; } = DateTime.Now;

        public string AppUserId { get; set; } 
        public AppUser AppUser { get; set; }

        public ICollection<Reserve> reserves { get; set; } = new HashSet<Reserve>();

    }
}
