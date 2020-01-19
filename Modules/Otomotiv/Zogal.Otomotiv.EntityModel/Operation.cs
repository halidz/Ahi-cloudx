using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Core;
using Zogal.Otomotiv.Core;

namespace Zogal.Otomotiv.EntityModel
{
    public class Operation :IEntity
    {
        public virtual long Id { get; set; }

        public virtual string Plate { get; set; }  //Vehicle propertysi eklenebilir.

        public virtual long OperationDate { get; set; } //YIL_AY_GUN

        public virtual long PeriodDate { get; set; } //YIL_AY_GUN

        public virtual long OperationTypeId { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; }

        public virtual decimal OperationAmount { get; set; }

        public virtual decimal CalculatedAmount { get; set; }

        public virtual decimal TipAmount { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string Gender { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual string VehicleType { get; set; }

        public virtual string VehicleModel { get; set; }

        public virtual string VehicleBrand { get; set; }

        public virtual Core.Status Status { get; set; }

        public virtual string CreatedBy { get; set; }
        public virtual string CreatedByFullName { get; set; }
        public virtual long CreatedDate { get; set; }  //YIL_AY_GUN_SAAT_DAKIKA_SANIYE 2 karakter for all

        public virtual string UpdatedBy { get; set; }
        public virtual string UpdatedByFullName { get; set; }
        public virtual long UpdatedDate { get; set; }

        public virtual string Description { get; set; }





    }
}
