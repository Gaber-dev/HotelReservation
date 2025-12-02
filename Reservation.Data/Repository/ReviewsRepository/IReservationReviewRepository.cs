using Reservation.Data.Model.ReservationReviews;
using Reservation.Data.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Repository.ReviewsRepository
{
    public interface IReservationReviewRepository  : IGenericRepository<ReservationReview>
    {
    }
}
