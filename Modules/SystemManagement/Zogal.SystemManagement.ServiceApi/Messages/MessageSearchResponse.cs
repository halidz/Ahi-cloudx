﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zogal.Core.ViewModel;
using Zogal.SystemManagement.ViewModel;

namespace Zogal.SystemManagement.ServiceApi.Messages
{
    public class MessageSearchResponse
    {
        public PaginatedList<MessageView> List { get; set; }

    }
}
