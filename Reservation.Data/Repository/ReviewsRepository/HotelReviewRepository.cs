using Reservation.Data.Data;
using Reservation.Data.Model.HotelReviews;
using Reservation.Data.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Repository.ReviewsRepository
{
    public class HotelReviewRepository : GenericRepository<HotelReview> , IHotelReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public HotelReviewRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }




    }
}
