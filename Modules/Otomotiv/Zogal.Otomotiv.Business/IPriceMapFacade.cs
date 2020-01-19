using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Core.ViewModel;
using Zogal.Otomotiv.ViewModel;

namespace Zogal.Otomotiv.Business
{
    public interface IPriceMapFacade
    {
        long Create(PriceView price);

        void Update(PriceView price);

        PriceView Get(long id);

        void Delete(long id);

        PaginatedList<PriceView> Search(PriceSearchFilter filter, PaginationInfoView paginationInfo);
    }
}
