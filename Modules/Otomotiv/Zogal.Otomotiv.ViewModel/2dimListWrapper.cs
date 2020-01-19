using System;
using System.Collections.Generic;
using System.Text;

namespace Zogal.Otomotiv.ViewModel
{
    public class _2dimListWrapper
    {
        public string Plate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public long OperationDate { get; set; }

        public List<MonthView> ListOfMonths { get; set; }
    }
}
