using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Core;

namespace Zogal.Otomotiv.ViewModel
{
    public class StockItemView
    {
        public long Id { get; set; }
        public long BarcodeNumber { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal NumberOfUnit { get; set; }
        public Status Status { get; set; }

        public long CreatedDate { get; set; }
    }
}
