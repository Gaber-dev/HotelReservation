using Reservation.Data.Model.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Model.Payments
{
    public class Payment
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        public double AmountPaid { get; set; }
        public int? TransactionId { get; set; }

        public string Status { get; set; }

        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}
