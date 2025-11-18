using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Dtos.ReservationDto
{
    public class CreateReservationResponseDto
    {
        public int ReservationId { get; set; }
        public int InvoiceId { get; set; }
        public int PaymentId { get; set; }
        public string PaymentIframeUrl { get; set; } = string.Empty;

        public string ConfirmationCode { get; set; }
    }
}
