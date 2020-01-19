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
    public class TipController : ControllerBase
    {
        ITipFacade _tipFacade;

        public TipController(ITipFacade tipFacade)
        {
            _tipFacade = tipFacade ?? throw new ArgumentNullException(nameof(tipFacade));
        }

        [HttpPost("create")]
        public TipCreateResponse Create(TipCreateRequest request)
        {
            var response = new TipCreateResponse();
            response.Id= _tipFacade.Create(request.Tip);
            return response;
        }


        [HttpPost("search")]
        public TipSearchResponse Search(TipSearchRequest request)
        {
            var response = new TipSearchResponse();
            response.List = _tipFacade.Search(request.Filter, request.PaginationInfo);
            return response;
        }

        [HttpPost("delete")]

        public TipDeleteResponse Delete(TipDeleteRequest request)
        {
            var response = new TipDeleteResponse();
            _tipFacade.Delete(request.Id);
            return response;
        }
    }
}