using Zogal.Core.ViewModel;
using Zogal.SystemManagement.ViewModel;

namespace Zogal.SystemManagement.Business
{
    public interface IRoleFacade
    {
        long Create(RoleItemView role);

        void Update(RoleItemView role);

        PaginatedList<RoleItemView> Search(RoleSearchFilter filter,PaginationInfoView paginationInfo);

        RoleItemView Get(long id);

        void Delete(long id);
    }
}
