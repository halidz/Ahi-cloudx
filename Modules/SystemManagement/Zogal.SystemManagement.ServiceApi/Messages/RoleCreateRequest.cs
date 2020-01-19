using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zogal.Core;
using Zogal.SystemManagement.ViewModel;

namespace Zogal.SystemManagement.ServiceApi.Messages
{
    public class RoleCreateRequest :ISecureRequest
    {
        public Guid TokenId { get; set; }
        public RoleItemView Role { get; set; }
    }
}
