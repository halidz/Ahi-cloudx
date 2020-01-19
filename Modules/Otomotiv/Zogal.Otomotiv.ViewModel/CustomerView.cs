using System;
using System.Collections.Generic;
using System.Text;

namespace Zogal.Otomotiv.ViewModel
{
    public class CustomerView
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public virtual string Gender { get; set; }
        public IList<SubscriberView> Subscribers { get; set; }
    }
}
