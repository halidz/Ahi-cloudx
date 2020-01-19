using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Otomotiv.Core;

namespace Zogal.Otomotiv.ViewModel
{
    public class OperationSearchFilter
    {
        public AccountType AccountType { get; set; }
        public string Plate { get; set; }

        public decimal Min { get; set; }

        public decimal Max { get; set; }

        public long OperationTypeId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }  //enum olabilir

        public long Date { get; set; }

        public long EndDate { get; set; }

        public long MonthlyDate { get; set; }

        public long Year { get; set; }

        public decimal OperationAmount { get; set; }

        public decimal TipAmount { get; set; }

        public PaymentInfo PaymentInfo { get; set; }

    }
}
