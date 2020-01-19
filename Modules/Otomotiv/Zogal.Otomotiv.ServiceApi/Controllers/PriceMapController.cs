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
    public class PriceMapController : ControllerBase
    {
        IPriceMapFacade _priceMapFacade;
        public PriceMapController(IPriceMapFacade priceMapFacade)
        {
            _priceMapFacade = priceMapFacade ?? throw new ArgumentNullException(nameof(priceMapFacade));
        }

        [HttpPost("create")]
       public PriceCreateResponse Create(PriceCreateRequest request)
        {
            var response = new PriceCreateResponse();
            response.Id= _priceMapFacade.Create(request.Price);

            return response;

        }   

        [HttpPost("update")]
        public PriceUpdateResponse Update(PriceUpdateRequest request)
        {
            var response = new PriceUpdateResponse();
            _priceMapFacade.Update(request.Price);

            return response;

        }

        [HttpPost("search")]

        public PriceSearchResponse Search(PriceSearchRequest request)
        {
            var response = new PriceSearchResponse();
            response.List=_priceMapFacade.Search(request.Filter, request.PaginationInfo);

            return response;

        }

        [HttpPost("detail")]
        public PriceGetResponse Get(PriceGetRequest request)
        {
            var response = new PriceGetResponse();
            response.Price = _priceMapFacade.Get(request.Id);


            return response;

        }

        [HttpPost("delete")]

        public PriceDeleteResponse Delete(PriceDeleteRequest request)
        {
            var response = new PriceDeleteResponse();

            _priceMapFacade.Delete(request.Id);

            return response;
        }



    }
}