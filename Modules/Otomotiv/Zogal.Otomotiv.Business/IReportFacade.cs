using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Core.ViewModel;
using Zogal.Otomotiv.ViewModel;

namespace Zogal.Otomotiv.Business
{
    public interface IReportFacade
    {

        PaginatedListN<WrappedCustomerReportView> CustomerReport(CustomerReportSearchFilter filter, PaginationInfoView paginationInfo);

        List<List<BalanceReportView>> BalanceReport(BalanceReportSearchFilter filter, PaginationInfoView paginationInfo);

        string BalanceReportPdf(BalanceReportSearchFilter filter, PaginationInfoView paginationInfo);
        List<OperationReportView> OperationReport(OperationReportSearchFilter filter, PaginationInfoView paginationInfo);

        string CustomerReportPdf(CustomerReportSearchFilter filter, PaginationInfoView paginationInfo);

    }
}
