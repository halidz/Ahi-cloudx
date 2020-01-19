using Zogal.Core.ViewModel;
using Zogal.SystemManagement.ViewModel;

namespace Zogal.SystemManagement.ServiceApi.Messages
{
    public class MessageSearchRequest
    {
        public MessageSearchFilter Filter { get; set; }
        
        public PaginationInfoView PaginationInfo { get; set; }
    }
}
