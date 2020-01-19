using Zogal.Core.ViewModel;
using Zogal.SystemManagement.ViewModel;

namespace Zogal.SystemManagement.Business
{
    public interface IMessageFacade
    {
        long Create(MessageView message);

        PaginatedList<MessageView> Search(MessageSearchFilter filter, PaginationInfoView paginationInfo);

        void Delete(long id);
    }
}
