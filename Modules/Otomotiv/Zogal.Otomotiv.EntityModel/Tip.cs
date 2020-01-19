using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Core;

namespace Zogal.Otomotiv.EntityModel
{
    public class Tip:IEntity
    {
        public virtual decimal TipAmount { get; set; }
        public virtual decimal WorkerNumber { get; set; }
        public virtual long Id { get ; set ; }

        public virtual long OperationDate { get; set; } //YIL_AY_GUN

        public virtual long PeriodDate { get; set; } //YIL_AY_GUN
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
