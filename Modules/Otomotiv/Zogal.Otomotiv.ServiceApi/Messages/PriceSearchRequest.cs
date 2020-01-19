﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zogal.Core;
using Zogal.Core.ViewModel;
using Zogal.Otomotiv.ViewModel;

namespace Zogal.Otomotiv.ServiceApi.Messages
{
    public class PriceSearchRequest:ISecureRequest
    {
        public Guid TokenId { get; set; }
        public PriceSearchFilter Filter { get; set; }

        public PaginationInfoView PaginationInfo { get; set; }
    }
}
