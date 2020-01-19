using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Core;
using Zogal.Otomotiv.Core;

namespace Zogal.Otomotiv.EntityModel
{
    public class Subscriber :IEntity
    {
        public virtual long Id { get; set; }  //one to mANY

        public virtual long CustomerId { get; set; }

        public virtual long StartDate { get; set; }

        public virtual long LastStartDate { get; set; }

        public virtual SubscriberStatus Status { get; set; }

        public virtual decimal MonthlySubscription { get; set; }

        public virtual decimal Deposit { get; set; }
        public virtual string Plate { get; set; }

        public virtual string Brand { get; set; }

        public virtual string Model { get; set; } //enum olabilir.yada sınıf

        public virtual string Type { get; set; } //enum olabilir.
                

        public virtual string CreatedBy { get; set; }
        public virtual string CreatedByFullName { get; set; }
        public virtual long CreatedDate { get; set; }
        public virtual string UpdatedBy { get; set; }
        public virtual string UpdatedByFullName { get; set; }
        public virtual long UpdatedDate { get; set; }

    }
}   //abonelik customerın propertysi olcak.
// burda list vehicle tek vehicle objesi olcak.
//Customer sınıfı yazılacak.

