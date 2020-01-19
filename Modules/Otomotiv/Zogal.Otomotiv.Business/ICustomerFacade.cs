using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Core.ViewModel;
using Zogal.Otomotiv.ViewModel;

namespace Zogal.Otomotiv.Business
{
    public interface ICustomerFacade
    {
        long Create(CustomerView customer);

        void Update(CustomerView customer);

        CustomerView Get(long id);

        void Delete(long id);

        PaginatedList<CustomerItemView> Search(CustomerSearchFilter filter, PaginationInfoView paginationInfo);
    }
}
