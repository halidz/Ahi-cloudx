using System;
using System.Collections.Generic;
using System.Text;

namespace Zogal.Core.ViewModel
{
    public class PaginationInfoView //seçimi sorguyu yapan belirliyor.
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string Order { get; set; }



    }
}
