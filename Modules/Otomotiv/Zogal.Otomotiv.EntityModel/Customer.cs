using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Core;

namespace Zogal.Otomotiv.EntityModel
{
    public class Customer : IEntity
    {
        public virtual long Id { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string Gender { get; set; }

        public virtual string CompanyName { get; set; }

        public virtual string PhoneNumber { get; set; }

        

        public virtual Status Status { get; set; }

        public virtual string CreatedBy { get; set; }
        public virtual string CreatedByFullName { get; set; }
        public virtual long CreatedDate { get; set; }

        public virtual string UpdatedBy { get; set; }
        public virtual string UpdatedByFullName { get; set; }
        public virtual long UpdatedDate { get; set; }

    }
}
