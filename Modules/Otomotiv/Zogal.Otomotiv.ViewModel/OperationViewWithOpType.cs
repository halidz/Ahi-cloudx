using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Otomotiv.Core;

namespace Zogal.Otomotiv.ViewModel
{
    public class OperationViewWithOpType
    {
        public long Id { get; set; }

        public string Plate { get; set; }    

        public string OperationType { get; set; }
      
        public long OperationDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public  string PhoneNumber { get; set; }

        public string VehicleType { get; set; }

        public string VehicleModel { get; set; }

        public string VehicleBrand { get; set; }

        public long PeriodDate { get; set; }

        public string PeriodMonth { get; set; }

        public decimal CalculatedAmount { get; set; }

        public PaymentMethod PaymentMethod { get; set; }  //enum olabilir

        public decimal OperationAmount { get; set; }

        public decimal TipAmount { get; set; }

        public string Description { get; set; }

        public long CreatedDate { get; set; }
    }
}
