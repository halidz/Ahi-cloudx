using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zogal.Core;
using Zogal.Core.ViewModel;
using Zogal.SystemManagement.ViewModel;

namespace Zogal.SystemManagement.ServiceApi.Messages
{
    public class UserSearchRequest : ISecureRequest
    {
        public Guid TokenId { get; set; }
        public UserSearchFilter Filter { get; set; }
        public PaginationInfoView PaginationInfo { get; set; }
    }
}
