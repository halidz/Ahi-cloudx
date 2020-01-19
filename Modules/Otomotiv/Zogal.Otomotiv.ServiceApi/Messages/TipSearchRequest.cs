using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zogal.Core.ViewModel;
using Zogal.Otomotiv.ViewModel;

namespace Zogal.Otomotiv.ServiceApi.Messages
{
    public class TipSearchRequest
    {
        public TipSearchFilter Filter { get; set; }
        public PaginationInfoView PaginationInfo { get; set; }
    }
}
