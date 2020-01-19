using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Core;
using Zogal.Otomotiv.Core;

namespace Zogal.Otomotiv.EntityModel
{
    public class OperationType : IEntity
    {
        public virtual long Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Code { get; set; }

        public virtual AccountType AccountType { get; set; }
    }
}
