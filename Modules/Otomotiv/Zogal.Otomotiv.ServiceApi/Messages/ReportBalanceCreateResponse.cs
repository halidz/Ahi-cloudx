using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zogal.Otomotiv.ViewModel;

namespace Zogal.Otomotiv.ServiceApi.Messages
{
    public class ReportBalanceCreateResponse
    {
        public List<List<BalanceReportView>> List { get; set;
        }
    }
}
