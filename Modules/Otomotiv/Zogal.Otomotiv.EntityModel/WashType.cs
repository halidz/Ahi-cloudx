﻿using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Core;

namespace Zogal.Otomotiv.EntityModel
{
    public class WashType :IEntity
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal DefaultPrice { get; set; }
        public virtual decimal ProcessDuration { get; set; }
    }
}
