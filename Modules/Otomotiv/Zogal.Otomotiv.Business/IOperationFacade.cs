using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Otomotiv.ViewModel;
using Zogal.Core;
using Zogal.Core.ViewModel;

namespace Zogal.Otomotiv.Business
{
    public interface IOperationFacade
    {
        long Create(OperationView daily);

        long MobileCreate(OperationView daily);

        void Update(OperationView daily);

        OperationView Get(long id);

        void Delete(long id);

        PaginatedList<OperationViewWithOpType> Search(OperationSearchFilter filter, PaginationInfoView paginationInfo);

        PaginatedList<OperationTypeView> SearchOperationType(OperationTypeFilter filter, PaginationInfoView paginationInfo);

        MobileStartOpView GetCustomerInfo(GetInfoFilter filter);

        decimal GetPrice(PriceCalculationFilter filter);
    }
}
