using Reservation.Data.Model.Payments;
using Reservation.Data.Model.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Model.Invoices
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime issuedate { get; set; }
        public double TotalAmount { get; set; }
        public double Discount { get; set; }
        public double TaxAmount { get; set; }

        public double NetAmount { get; set; }

        public ICollection<Payment> payments { get; set; } = new HashSet<Payment>();

        public int ReserveId { get; set; }
        public Reserve Reserve { get; set; }

    }
}
