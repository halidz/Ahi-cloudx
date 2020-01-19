using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Otomotiv.Core;

namespace Zogal.Otomotiv.ViewModel
{
    public class MobileStartOpView
    { 

          public long CustomerId { get; set; }
          public long SubscriberId { get; set; }

          public string FirstName { get; set; }

          public string LastName { get; set; }

          public string PhoneNumber { get; set; }
          
          public string Gender { get; set; }

          public decimal CalculatedAmount { get; set; }

          public long PaidAmount { get; set; }

          public PaymentMethod PaymentMethod { get; set; }
    
          public SubscriberStatus Status { get; set; }

          public string Plate { get; set; }

          public string Model { get; set; }

          public string VehicleType { get; set; }

          public string VehicleBrand { get; set; }

          public string Description { get; set; }

    }
}
