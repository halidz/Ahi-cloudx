﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zogal.Core;
using Zogal.Core.ViewModel;
using Zogal.Otomotiv.ViewModel;

namespace Zogal.Otomotiv.ServiceApi.Messages
{
    public class SubscriberSearchRequest : ISecureRequest
    {
        public Guid TokenId { get; set; }
        public SubscriberSearchFilter Filter { get; set; }
        public PaginationInfoView PaginationInfo { get; set; }
    }
}
