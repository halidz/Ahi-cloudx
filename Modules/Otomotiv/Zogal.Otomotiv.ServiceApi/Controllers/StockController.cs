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
    public class StockController : ControllerBase
    {
        IStockFacade _stockFacade;

        public StockController(IStockFacade stockFacade)
        {
            _stockFacade = stockFacade ?? throw new ArgumentNullException(nameof(stockFacade));
        }

        [HttpPost("create")]
        public StockCreateResponse Create(StockCreateRequest request)
        {
            var response = new StockCreateResponse();
            response.Id= _stockFacade.Create(request.StockItem);
            return response;


        }

        [HttpPost("delete")]

        public StockDeleteResponse Delete (StockDeleteRequest request)
        {
            var response = new StockDeleteResponse();

            _stockFacade.Delete(request.Id);

            return response;
        }

        [HttpPost("search")]

        public StockSearchResponse Search(StockSearchRequest request)
        {
            var response = new StockSearchResponse();

            response.List=_stockFacade.Search(request.Filter, request.PaginationInfo);

            return response;
        }
        
    }
}