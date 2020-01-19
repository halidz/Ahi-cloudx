using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Core.ViewModel;
using Zogal.Otomotiv.ViewModel;

namespace Zogal.Otomotiv.Business
{
    public interface ITipFacade
    {
        long Create(TipView tip);

        TipView Get(long id);

        PaginatedList<TipView> Search(TipSearchFilter filter, PaginationInfoView paginationInfo);

        void Delete(long id);
    }
}
