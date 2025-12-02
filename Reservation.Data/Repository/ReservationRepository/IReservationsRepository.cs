using Reservation.Data.Model.Reservations;
using Reservation.Data.Repository.GenericRepository;
using Reservation.Domain.Dtos.ReservationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Repository.ReservationRepository
{
    public interface IReservationsRepository  : IGenericRepository<Reserve>
    {
        Task<int> GetTotalReservatioCount(string userId);
        Task<double> GetTotalReservatioMoneyspent(string userId);
        Task<List<GetCurrentReservationsDto>> GetCurrentReservations(string userId);
        Task<List<GetReservationHistoryDto>> GetReservationsHistory(string userId);
        Task<List<MyReservationsDto>> MyReservationsAll(string userId);
    }
}

