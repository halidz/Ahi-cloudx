using System;
using System.Collections.Generic;
using System.Text;

namespace Zogal.Otomotiv.ViewModel
{
    public class SubscriberSearchFilter
    {
        public long CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Plate { get; set; }

        public string PhoneNumber { get; set; }

        public string Gender { get; set; }
    }
}
