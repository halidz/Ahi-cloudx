using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Otomotiv.Core;

namespace Zogal.Otomotiv.ViewModel
{
    public class OperationTypeView
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public AccountType AccountType { get; set; }
    }
}
