﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zogal.Core;
using Zogal.Otomotiv.ViewModel;

namespace Zogal.Otomotiv.ServiceApi.Messages
{
    public class CustomerUpdateRequest : ISecureRequest
    {
        public Guid TokenId { get; set; }
        public CustomerView Customer { get; set; }
    }
}
