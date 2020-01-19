using System;
using System.Collections.Generic;
using System.Text;

namespace Zogal.Otomotiv.ViewModel
{
    public class TipSearchFilter
    {
        public decimal TipAmount { get; set; }
        public decimal WorkerNumber { get; set; }
        public long Id { get; set; }
        public long OperationDate { get; set; } //YIL_AY_GUN
        public long PeriodDate { get; set; } //YIL_AY_GUN
    }
}
