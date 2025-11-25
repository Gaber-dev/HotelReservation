using Microsoft.AspNetCore.Identity;
using Reservation.Data.Data;
using Reservation.Data.Model.ReservationReviews;
using Reservation.Data.Model.User;
using Reservation.Data.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Repository.ReviewsRepository
{
    public class ReservationReviewRepository : GenericRepository<ReservationReview> , IReservationReviewRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _user;

        public ReservationReviewRepository(ApplicationDbContext context , UserManager<AppUser> user) : base(context) 
        {
            _context = context;
            _user = user;
        }


    }
}
