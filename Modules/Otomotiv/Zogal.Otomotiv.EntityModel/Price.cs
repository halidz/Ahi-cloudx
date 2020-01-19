using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Core;


namespace Zogal.Otomotiv.EntityModel
{
    public class Price:IEntity
    {
        public virtual long Id { get; set; }
        public virtual string VehicleType { get; set;}
        public virtual long OperationTypeId { get; set; }
        public virtual decimal DefaultPrice { get; set; }

        public virtual Status Status { get; set; }

        public virtual string CreatedBy { get; set; }
        public virtual string CreatedByFullName { get; set; }
        public virtual long CreatedDate { get; set; }  //YIL_AY_GUN_SAAT_DAKIKA_SANIYE 2 karakter for all

        public virtual string UpdatedBy { get; set; }
        public virtual string UpdatedByFullName { get; set; }
        public virtual long UpdatedDate { get; set; }
    }
}
