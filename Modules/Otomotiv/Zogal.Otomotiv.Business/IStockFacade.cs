using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Core.ViewModel;
using Zogal.Otomotiv.ViewModel;

namespace Zogal.Otomotiv.Business
{
    public interface IStockFacade
    {
        long Create(StockItemView stockItem);

        void Update(StockItemView stockItem);

        StockItemView Get(long id);

        void Delete(long id);

        PaginatedList<StockItemView> Search(StockSearchFilter filter, PaginationInfoView paginationInfo);
    }
}
