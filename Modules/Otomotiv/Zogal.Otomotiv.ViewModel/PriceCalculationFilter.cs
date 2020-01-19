using System;
using System.Collections.Generic;
using System.Text;

namespace Zogal.Otomotiv.ViewModel
{
    public class PriceCalculationFilter
    {
        public string VehicleType { get; set; }

        public long OperationTypeId { get; set; }

        public decimal CustomerDiscount { get; set; }

    }
}
