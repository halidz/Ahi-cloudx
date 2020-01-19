using System;
using System.Collections.Generic;
using System.Text;

namespace Zogal.Otomotiv.ViewModel
{
    public class CustomerReportSearchFilter
    {
        public string Plate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public long Year { get; set; }

        public long OperationTypeId { get; set; }

    }
}
