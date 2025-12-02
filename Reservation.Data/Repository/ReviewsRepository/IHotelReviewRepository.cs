using Reservation.Data.Model.HotelReviews;
using Reservation.Data.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Repository.ReviewsRepository
{
    public interface IHotelReviewRepository : IGenericRepository<HotelReview>
    {

    }
}
