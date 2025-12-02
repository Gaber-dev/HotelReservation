using Reservation.Data.Model.HotelReviews;
using Reservation.Data.Model.ReservationReviews;
using Reservation.Data.Repository.ReviewsRepository;
using Reservation.Domain.Dtos.ReviewsDto.HotelReviewsDto;

namespace Reservation.API.Managers.ReviewsManager
{
    public class ReviewService  : IReviewService
    {
        private readonly IHotelReviewRepository _hotelreviewrepo;
        private readonly IReservationReviewRepository _reservationrepo;

        public ReviewService(IHotelReviewRepository hotelreviewrepo , IReservationReviewRepository reservationrepo)
        {
            _hotelreviewrepo = hotelreviewrepo;
            _reservationrepo = reservationrepo;
        }

        public async Task<string> AddHotelReview(HotelReviewDto dto , string userId)
        {
            var hotelReviewToDB = new HotelReview
            {
              Rating  = dto.Rating,
              Comment = dto.Comment,
              AppUserId = userId,
              CreatedAt = DateTime.Now
            };
            await _hotelreviewrepo.Add(hotelReviewToDB);
            return "Thanks for your review we will call you soon.";
        }

        public async Task<string> AddReservationReview(HotelReviewDto dto, string userId , int reservationId)
        {
            var ReservationReviewToDB = new ReservationReview
            {
                Rating = dto.Rating,
                Comment = dto.Comment,
                AppUserId = userId,
                CreatedAt = DateTime.Now,
                ReserveId = reservationId
            };
            await _reservationrepo.Add(ReservationReviewToDB);
            return "Thanks for your review we will call you soon.";
        }

    }
}
