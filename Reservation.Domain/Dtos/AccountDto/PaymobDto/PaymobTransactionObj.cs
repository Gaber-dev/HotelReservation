using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Dtos.PaymobDto
{
    public class PaymobTransactionObj
    {
        public int Id { get; set; }                    
        public bool Success { get; set; }
        public int AmountCents { get; set; }
        public bool IsPaid { get; set; }
        public PaymobOrderInfo Order { get; set; } = null!;
        public int IntegrationId { get; set; }
    }
}
