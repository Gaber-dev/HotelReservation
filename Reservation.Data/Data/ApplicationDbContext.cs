using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Reservation.Data.Model.Amenities;
using Reservation.Data.Model.ContactUs;
using Reservation.Data.Model.Guests;
using Reservation.Data.Model.HotelReviews;
using Reservation.Data.Model.Invoices;
using Reservation.Data.Model.Payments;
using Reservation.Data.Model.ReservationReviews;
using Reservation.Data.Model.Reservations;
using Reservation.Data.Model.Role;
using Reservation.Data.Model.RoomAmenities;
using Reservation.Data.Model.RoomImages;
using Reservation.Data.Model.Rooms;
using Reservation.Data.Model.Roomtype;
using Reservation.Data.Model.User;
using Reservation.Data.Model.UserAddress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser,AppRole,string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Amenity> Amenity { get; set; }
        public DbSet<RoomAmenity> RoomAmenity { get; set; }
        public DbSet<Guest> Guest { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<Reserve> Reservations { get; set; }
        public DbSet<HotelReview> HotelReviews { get; set; }
        public DbSet<ReservationReview> ReservationReviews { get; set; }
        public DbSet<Contact> ContactUs { get; set; }

        public DbSet<Address> Address { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Reserve>()
                .HasOne(r => r.Guest)
                .WithMany(g => g.reserves)
                .HasForeignKey(r => r.GuestId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ReservationReview>()
                .HasOne(rr => rr.Reserve)
                .WithOne(r => r.ReservationReview)
                .HasForeignKey<ReservationReview>(rr => rr.ReserveId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
