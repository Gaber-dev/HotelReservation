using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Dtos.PaymobDto
{
    public class TransactionObj
    {
        public int id { get; set; }                 // Transaction ID
        public bool success { get; set; }
        public int amount_cents { get; set; }
        public bool is_paid { get; set; }
        public OrderInfo order { get; set; } = null!;
        public int integration_id { get; set; }
    }
}
