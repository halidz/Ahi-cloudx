using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Core.ViewModel;
using Zogal.Otomotiv.ViewModel;

namespace Zogal.Otomotiv.Business
{
    public interface ISubscriberFacade
    {
        long Create(SubscriberItemView subscriber);

        long Update(SubscriberItemView subscriber);

        SubscriberItemView Get(long id);

        void Delete(long id);

        PaginatedList<SubscriberItemView> Search(SubscriberSearchFilter filter, PaginationInfoView paginationInfo);

        PaginatedList<MobileStartOpView> MobileSearch(MobileSubscriberSearchFilter filter, PaginationInfoView paginationInfo);

      

    }
}
