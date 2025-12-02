using Reservation.Domain.Dtos.ReviewsDto.HotelReviewsDto;

namespace Reservation.API.Managers.ReviewsManager
{
    public interface IReviewService
    {
        Task<string> AddHotelReview(HotelReviewDto dto, string userId);
        Task<string> AddReservationReview(HotelReviewDto dto, string userId, int reservationId);
    }
}
