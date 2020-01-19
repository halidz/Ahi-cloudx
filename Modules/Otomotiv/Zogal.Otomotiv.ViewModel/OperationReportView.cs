using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Otomotiv.Core;

namespace Zogal.Otomotiv.ViewModel
{
    public class OperationReportView
    {
        public string OperationName { get; set; }

        public string UniqueName { get; set; }

        public decimal TotalOpCount { get; set; }

        public decimal CashOpCount { get; set; }

        public decimal CreditCardOpCount { get; set; }

        public decimal CashSum { get; set; }

        public decimal CreditCardSum { get; set; }

        public decimal TotalSum { get; set; }

        public AccountType AccountType { get; set; }

        public List<OperationViewWithOpType> List { get; set; }
    }
}
