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
    public class MobileController : ControllerBase
    {
        IOperationFacade _operationFacade;
        ISubscriberFacade _subscriberFacade;
        public MobileController(IOperationFacade operationFacade, ISubscriberFacade subscriberFacade)
        {

            _operationFacade = operationFacade ?? throw new ArgumentNullException(nameof(operationFacade));
            _subscriberFacade = subscriberFacade ?? throw new ArgumentNullException(nameof(subscriberFacade));
        }

        [HttpPost("CalculatePrice")]
        MobileCalculatePriceResponse CalculatePrice(MobileCalculatePriceRequest request)
        {
            var response = new MobileCalculatePriceResponse();

            var entity = _subscriberFacade.MobileSearch(request.Filter, request.PaginationInfo).List.First();
            if (entity != null)
                response.View = entity;
            return response;
        }

        [HttpPost("SaveOperation")]

        MobileSaveOperationResponse SaveOperation(MobileSaveOperationRequest request)
        {
            var response = new MobileSaveOperationResponse();
            return response;
        }


    }
}