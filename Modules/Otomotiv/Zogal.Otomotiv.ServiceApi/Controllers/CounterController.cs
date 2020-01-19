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
    public class CounterController : ControllerBase
    {
        ICounterFacade _counterFacade;
      

        public CounterController(ICounterFacade counterFacade)
        {
            _counterFacade = counterFacade ?? throw new ArgumentNullException(nameof(counterFacade));
        }

        [HttpPost("create")]
        public CounterCreateResponse Create(CounterCreateRequest request)
        {
            var response = new CounterCreateResponse();

            response.Id = _counterFacade.Create(request.Counter);

            return response;
        }

        [HttpPost("get")]
        public CounterGetResponse Get(CounterGetRequest request)
        {
            var response = new CounterGetResponse();

            response.Counter = _counterFacade.Get(request.Id);

            return response;
        }

        [HttpPost("search")]
        public CounterSearchResponse Search(CounterSearchRequest request)
        {
            var response = new CounterSearchResponse();

            response.List = _counterFacade.Search(request.Filter, request.PaginationInfo);

            return response;
        }

        [HttpPost("delete")]

        public CounterDeleteResponse Delete(CounterDeleteRequest request)
        {
            var response = new CounterDeleteResponse();

            _counterFacade.Delete(request.Id);

            return response;
        }

    }
}