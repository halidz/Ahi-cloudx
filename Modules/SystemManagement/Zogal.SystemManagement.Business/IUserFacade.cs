using Zogal.Core.ViewModel;
using Zogal.SystemManagement.ViewModel;

namespace Zogal.SystemManagement.Business
{
    public interface IUserFacade
    {
        long Create(UserEditView user);
        void Update(UserEditView user);
        PaginatedList<UserItemView> Search(UserSearchFilter filter,PaginationInfoView paginationInfo);
        UserDetailView Get(long id);
        void Delete(long id);
    }
}
