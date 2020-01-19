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
    public class OperationTypeController : ControllerBase
    {
        IOperationTypeFacade _operationTypeFacade;

        public OperationTypeController(IOperationTypeFacade operationTypeFacade)
        {
            _operationTypeFacade = operationTypeFacade ?? throw new ArgumentNullException(nameof(operationTypeFacade));
        }

        [HttpPost("create")]
        public OperationTypeCreateResponse Create(OperationTypeCreateRequest request)
        {
            var response = new OperationTypeCreateResponse();
            response.Id = _operationTypeFacade.Create(request.OperationType);
            return response;
        }


        [HttpPost("search")]
        public OperationTypeSearchResponse Search(OperationTypeSearchRequest request)
        {
            var response = new OperationTypeSearchResponse();
            response.List = _operationTypeFacade.Search(request.Filter, request.PaginationInfo);
            return response;
        }

        [HttpPost("delete")]

        public OperationTypeDeleteResponse Delete(OperationTypeDeleteRequest request)
        {
            var response = new OperationTypeDeleteResponse();
            _operationTypeFacade.Delete(request.Id);
            return response;
        }

        [HttpPost("detail")]

        public OperationTypeGetResponse Get(OperationTypeGetRequest request)
        {
            var response = new OperationTypeGetResponse();
            response.OperationType = _operationTypeFacade.Get(request.Id);
            return response;
        }

        [HttpPost("update")]

        public OperationTypeUpdateResponse Delete(OperationTypeUpdateRequest request)
        {
            var response = new OperationTypeUpdateResponse();
            _operationTypeFacade.Update(request.OperationType);
            return response;
        }
    }
}