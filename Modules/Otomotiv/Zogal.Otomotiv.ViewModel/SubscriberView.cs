using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Otomotiv.Core;

namespace Zogal.Otomotiv.ViewModel
{
    public class SubscriberView
    {
        public long Id { get; set; }

        public long StartDate { get; set; }

        public int MonthlySubscription { get; set; }

        public SubscriberStatus Status { get; set; }


        public string Plate { get; set; }

        public string Model { get; set; } //enum olabilir.yada sınıf

        public string Type { get; set; } //enum olabilir.



    }
}
