using System;
using System.Collections.Generic;
using System.Text;

namespace Zogal.Otomotiv.ViewModel
{
    public class MonthView
    {
        public long PeriodDate { get; set; }

        public decimal OperationAmount { get; set; }

        public virtual string Description { get; set; }
    }
}
