using System;
using System.Collections.Generic;
using System.Text;

namespace Zogal.Otomotiv.ViewModel
{
    public class MobileSubscriberSearchFilter:SubscriberSearchFilter
    {
        public string VehicleType { get; set; }

        public string OperationType { get; set; }

     
        public decimal CustomerDiscount { get; set; }
    }
}
