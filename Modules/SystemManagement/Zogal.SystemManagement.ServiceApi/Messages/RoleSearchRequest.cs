using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zogal.Core;
using Zogal.Core.ViewModel;
using Zogal.SystemManagement.ViewModel;

namespace Zogal.SystemManagement.ServiceApi.Messages
{
    public class RoleSearchRequest : ISecureRequest
    {
        public Guid TokenId { get; set; }
        public RoleSearchFilter Filter { get; set; }

        public PaginationInfoView PaginationInfo { get; set; }
    }
}
