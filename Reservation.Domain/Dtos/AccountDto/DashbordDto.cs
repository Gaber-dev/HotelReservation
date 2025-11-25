using Reservation.Domain.Dtos.ReservationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Dtos.AccountDto
{
    public class DashbordDto
    {
        public string imageUrl {  get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public int TotalReservation {  get; set; }

        public double TotalSpent { get; set; }
        public string WelcomeString { get; set; }

        public List<GetCurrentReservationsDto> CurrentReservations { get; set; }

        public List<GetReservationHistoryDto> GetReservationHistory { get; set; }
    }
}
