using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zogal.Otomotiv.Business;
using Zogal.Otomotiv.ServiceApi.Messages;

namespace Zogal.Otomotiv.ServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportFacade _reportFacade;
        private readonly IFileManagerFacade _fileManagerFacade;
        public ReportController(IReportFacade reportFacade, IFileManagerFacade fileManagerFacade)
        {
            _reportFacade = reportFacade ?? throw new ArgumentNullException(nameof(reportFacade));
            _fileManagerFacade = fileManagerFacade ?? throw new ArgumentNullException(nameof(fileManagerFacade));
        }


        [HttpPost("customerReport")]
        public ReportCustomerCreateResponse Create(ReportCustomerCreateRequest request)
        {
            var response = new ReportCustomerCreateResponse();

            response.List= _reportFacade.CustomerReport(request.Filter,request.PaginationInfo);

            return response;
        }

        [HttpPost("balanceReport")]

        public ReportBalanceCreateResponse CreateBalance(ReportBalanceCreateRequest request)
        {
            var response = new ReportBalanceCreateResponse();

            response.List = _reportFacade.BalanceReport(request.Filter, request.PaginationInfo);

            return response;
        }

        [HttpPost("operationReport")]

        public ReportOperationCreateResponse CreateOperationReport(ReportOperationCreateRequest request)
        {
            var response = new ReportOperationCreateResponse();

            response.List = _reportFacade.OperationReport(request.Filter, request.PaginationInfo);

            return response;
        }

        [HttpPost("balanceReportPdf")]

        public ReportBalancePdfResponse CreateBalanceReportPdf(ReportBalanceCreateRequest request)
        {
            var response = new ReportBalancePdfResponse();
            response.Base64=_reportFacade.BalanceReportPdf(request.Filter,request.PaginationInfo);
            return response;
        }

        [HttpPost("customerReportPdf")]

        public ReportCustomerPdfResponse CreateCustomerReportPdf(ReportCustomerPdfRequest request)
        {
            var response = new ReportCustomerPdfResponse();
            response.Base64 = _reportFacade.CustomerReportPdf(request.Filter, request.PaginationInfo);
            return response;

        }
    }
}