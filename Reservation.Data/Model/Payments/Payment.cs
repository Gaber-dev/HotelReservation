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
        public string PaymentMethod { get; set; } = "Card";
        public string Provider { get; set; } = "Paymob";
        public DateTime? PaymentDate { get; set; } 
        public double AmountPaid { get; set; }
        public string? TransactionId { get; set; }       
        public string? PaymobOrderId { get; set; }   
        public string Currency { get; set; } = "EGP";
        public string Status { get; set; } = "Pending"; 
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; } = null!;
    }
}
