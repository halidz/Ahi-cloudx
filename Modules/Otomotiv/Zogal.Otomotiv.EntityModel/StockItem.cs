using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Core;

namespace Zogal.Otomotiv.EntityModel
{
    public class StockItem :IEntity
    {
        public virtual long Id { get ; set ; }
        public virtual long BarcodeNumber { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal UnitPrice { get; set; }     
        public virtual decimal NumberOfUnit { get; set; }
        public virtual Status Status { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual string CreatedByFullName { get; set; }
        public virtual long CreatedDate { get; set; }
        public virtual string UpdatedBy { get; set; }
        public virtual string UpdatedByFullName { get; set; }
        public virtual long UpdatedDate { get; set; }
    }
}
