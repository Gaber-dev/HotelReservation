using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Dtos.PaymobDto
{
    public class PaymobCallbackDto
    {
        public TransactionObj obj { get; set; } = null!;
        public string hmac { get; set; } = "";
    }
}
