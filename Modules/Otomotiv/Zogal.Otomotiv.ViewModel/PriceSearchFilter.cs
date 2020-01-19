using System;
using System.Collections.Generic;
using System.Text;

namespace Zogal.Otomotiv.ViewModel
{
    public class PriceSearchFilter
    {
        public virtual string VehicleType { get; set; }
        public virtual long OperationTypeId { get; set; }

        public virtual string OperationTypeCode { get; set; }
        public virtual decimal DefaultPrice { get; set; }
    }
}
