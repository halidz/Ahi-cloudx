using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Core;

namespace Zogal.Otomotiv.ViewModel
{
    public class PriceView
    {
        public  long Id { get; set; }
        public  string VehicleType { get; set; }
        public  long OperationTypeId { get; set; }
        public string OperationName { get; set; }
        public decimal DefaultPrice { get; set; }
        public Status Status { get; set; }
        
    }
}
