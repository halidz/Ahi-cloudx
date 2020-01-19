using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Core.ViewModel;
using Zogal.Otomotiv.ViewModel;

namespace Zogal.Otomotiv.Business
{
    public interface IOperationTypeFacade
    {
        long Create(OperationTypeView operationType);

        OperationTypeView Get(long id);

        PaginatedList<OperationTypeView> Search(OperationTypeFilter filter, PaginationInfoView paginationInfo);

        void Delete(long id);

        void Update(OperationTypeView operationType);
    }
}
