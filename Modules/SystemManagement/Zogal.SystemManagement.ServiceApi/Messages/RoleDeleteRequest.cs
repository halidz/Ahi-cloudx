﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zogal.Core;

namespace Zogal.SystemManagement.ServiceApi.Messages
{
    public class RoleDeleteRequest : ISecureRequest
    {
        public Guid TokenId { get; set; }
        public long Id { get; set; }
    }
}
