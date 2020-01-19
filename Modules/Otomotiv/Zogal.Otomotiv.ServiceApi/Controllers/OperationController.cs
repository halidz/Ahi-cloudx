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
    public class OperationController : ControllerBase
    {
        IOperationFacade _facade;

        public OperationController(IOperationFacade facade)
        {
            _facade = facade ?? throw new ArgumentNullException(nameof(facade));
        }


        [HttpPost("create")]
        public OperationCreateResponse Create([FromBody]OperationCreateRequest request)
        {
            var response = new OperationCreateResponse();

            response.Id = _facade.Create(request.Operation);

            return response;

        }


        [HttpPost("search")]
        public OperationSearchResponse Search(OperationSearchRequest request)
        {
            var response = new OperationSearchResponse();

            response.List = _facade.Search(request.Filter, request.PaginationInfo);

            return response;
        }


        [HttpPost("detail")]

        public OperationGetResponse Get(OperationGetRequest request)
        {
            var response = new OperationGetResponse();
            response.Operation = _facade.Get(request.Id);
            return response;
        }

        [HttpPost("delete")]

        public OperationDeleteResponse Delete(OperationDeleteRequest request)
        {
            var response = new OperationDeleteResponse();
            _facade.Delete(request.Id);
            return response;
        }

        [HttpPost("getInfo")]

        public OperationInfoResponse GetInfo(OperationInfoRequest request)
        {
            var response = new OperationInfoResponse();
            response.Operation = _facade.GetCustomerInfo(request.Filter);
            return response;
        }

        [HttpPost("searchOpType")]

        public OperationTypeSearchResponse SearchOpType(OperationTypeSearchRequest request)
        {
            var response = new OperationTypeSearchResponse();
            response.List = _facade.SearchOperationType(request.Filter, request.PaginationInfo);
            return response;
        }

       
    }
}