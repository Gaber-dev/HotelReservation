using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reservation.Domain.Dtos.Pricesdto;

namespace Reservation.Domain.Dtos.Pricesdto
{
    public class CalculatePriceDto
    {
        public double PricePerNight { get; set; }
        public int NumberOfGuests { get; set; }

        public int subtotals { get; set; }
        public double TaxesAndFees { get; set; }
        public double ServiceCharge { get; set; }
        public double TotalAmount { get; set; }

    }
}
