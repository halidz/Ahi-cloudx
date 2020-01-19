using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Core;

namespace Zogal.Otomotiv.EntityModel
{
    public class Counter :IEntity
    {
        public virtual long Id { get; set; }

        public virtual string Type { get; set; }

        public virtual decimal StartValue { get; set; }

        public virtual decimal StopValue { get; set; }

        public virtual decimal SpentValue { get; set; }

        public virtual long Date { get; set; }

        public virtual Status Status { get; set; }

        public virtual string CreatedBy { get; set; }
        public virtual string CreatedByFullName { get; set; }
        public virtual long CreatedDate { get; set; }

        public virtual string UpdatedBy { get; set; }
        public virtual string UpdatedByFullName { get; set; }
        public virtual long UpdatedDate { get; set; }
    }
}
