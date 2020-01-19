using System;
using System.Collections.Generic;
using System.Text;

namespace Zogal.Otomotiv.ViewModel
{
    public class CustomerReportView
    {
        public long CustomerId { get; set; }
        public string Plate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public long OperationDate { get; set; }

        public long PeriodDate { get; set; }

        public decimal OperationAmount { get; set; }

        public virtual string Description { get; set; }
    }
}
