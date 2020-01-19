using Zogal.Otomotiv.Core;

namespace Zogal.Otomotiv.ViewModel
{
    public class SubscriberItemView
    {
        public long CustomerId { get; set; }
        public long SubscriberId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Gender { get; set; }

        public decimal Deposit { get; set; }
        public decimal MonthlySubscription { get; set; }

        public long StartDate { get; set; }

        public SubscriberStatus Status { get; set; }

        public string Plate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
    }
}
