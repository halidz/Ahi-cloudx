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
    public class SubscriberController : ControllerBase
    {

        ISubscriberFacade _subscriberFacade;

        public SubscriberController(ISubscriberFacade subscriberFacade)
        {
            _subscriberFacade = subscriberFacade ?? throw new ArgumentNullException(nameof(subscriberFacade));

        }


        [HttpPost("search")]

        public SubscriberSearchResponse Search(SubscriberSearchRequest request)
        {
            var response = new SubscriberSearchResponse();

            response.List =_subscriberFacade.Search(request.Filter, request.PaginationInfo);

            return response;
        }

        [HttpPost("delete")]

        public SubscriberDeleteResponse Delete(SubscriberDeleteRequest request)
        {
            var response = new SubscriberDeleteResponse();

            _subscriberFacade.Delete(request.Id);

            return response;
        }

        [HttpPost("create")]

        public SubscriberCreateResponse Create(SubscriberCreateRequest request)
        {
            var response = new SubscriberCreateResponse();          

            response.Id=_subscriberFacade.Create(request.Subscriber);

            return response;

        }

        [HttpPost("detail")]

        public SubscriberGetResponse Get(SubscriberGetRequest request)
        {
            var response = new SubscriberGetResponse();
            response.Subscriber= _subscriberFacade.Get(request.Id);
            return response;

        }

        [HttpPost("update")]

        public SubscriberUpdateResponse Update(SubscriberUpdateRequest request)
        {
            var response = new SubscriberUpdateResponse();
            response.Id=_subscriberFacade.Update(request.Subscriber);

            return response;
        }

    }
}