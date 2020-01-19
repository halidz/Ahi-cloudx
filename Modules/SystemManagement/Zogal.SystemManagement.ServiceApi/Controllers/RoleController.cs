using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zogal.SystemManagement.Business;
using Zogal.SystemManagement.ServiceApi.Messages;

namespace Zogal.SystemManagement.ServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        IRoleFacade _facade;
        ITokenFacade _tokenFacade;
        public RoleController(IRoleFacade roleFacade, ITokenFacade tokenFacade)
        {
            _facade = roleFacade ?? throw new ArgumentNullException(nameof(roleFacade));
            _tokenFacade = tokenFacade ?? throw new ArgumentNullException(nameof(tokenFacade));
        }



        [HttpPost("create")]
        public RoleCreateResponse Create(RoleCreateRequest request)
        {
            var token = _tokenFacade.GetWithTokenId(request.TokenId);
            if (token == null)
                return null;

            var response = new RoleCreateResponse();

            response.Id = _facade.Create(request.Role);

            return response;
        }


        [HttpPost("update")]
        public RoleUpdateResponse Update(RoleUpdateRequest request)
        {
            var token = _tokenFacade.GetWithTokenId(request.TokenId);
            if (token == null)
                return null;

            var response = new RoleUpdateResponse();
            _facade.Update(request.Role);
            return response;
        }


        [HttpPost("search")]
        public RoleSearchResponse Search(RoleSearchRequest request)
        {
         

            var response = new RoleSearchResponse();
            response.List = _facade.Search(request.Filter, request.PaginationInfo);

            return response;
        }

        [HttpPost("detail")]
        public RoleGetResponse Get (RoleGetRequest request)
        {
            var token = _tokenFacade.GetWithTokenId(request.TokenId);
            if (token == null)
                return null;

            var response = new RoleGetResponse();
            response.Role = _facade.Get(request.Id);
            return response;
        }

        [HttpPost("delete")]

        public RoleDeleteResponse Delete([FromBody]RoleDeleteRequest request)
        {
            var token = _tokenFacade.GetWithTokenId(request.TokenId);
            if (token == null)
                return null;

            var response = new RoleDeleteResponse();
            _facade.Delete(request.Id);
            return response;

        }


    }
}
