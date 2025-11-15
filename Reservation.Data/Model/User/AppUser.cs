using Microsoft.AspNetCore.Identity;
using Reservation.Data.Model.ContactUs;
using Reservation.Data.Model.Guests;
using Reservation.Data.Model.HotelReviews;
using Reservation.Data.Model.ReservationReviews;
using Reservation.Data.Model.Reservations;
using Reservation.Data.Model.Role;
using Reservation.Data.Model.UserAddress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Model.User
{   
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string? ProfileImage { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? ExpityAt { get; set; }
        public string? ResetCode { get; set; }
        public ICollection<Guest> guests { get; set; } = new HashSet<Guest>();
        public ICollection<Address> address { get; set; } = new HashSet<Address>();

        public ICollection<Reserve> reservations { get; set; } = new HashSet<Reserve>();
        public ICollection<HotelReview> HotelReviews { get; set; } = new HashSet<HotelReview>();
        public ICollection<ReservationReview> ReservationReview { get; set; } = new HashSet<ReservationReview>();

        public ICollection<Contact> ContactUS { get; set; } = new HashSet<Contact>();

    }
}
