using Zogal.Core.ViewModel;
using Zogal.Otomotiv.ViewModel;

namespace Zogal.Otomotiv.Business
{
    public interface ICounterFacade
    {
        long Create(CounterView counter);

        CounterView Get(long id);

        PaginatedList<CounterView> Search(CounterSearchFilter filter, PaginationInfoView paginationInfo);

        void Delete(long id);
    }
}
