using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zogal.SystemManagement.Business;
using Zogal.SystemManagement.ServiceApi.Messages;

namespace Zogal.SystemManagement.ServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserFacade _facade;
        ITokenFacade _tokenFacade;

        public UserController(IUserFacade facade, ITokenFacade tokenFacade)
        {
            _facade = facade ?? throw new ArgumentNullException(nameof(facade));
            _tokenFacade = tokenFacade ?? throw new ArgumentNullException(nameof(tokenFacade));
        }

        [HttpPost("create")]
        public UserCreateResponse Create([FromBody]UserCreateRequest request)
        {
            var token = _tokenFacade.GetWithTokenId(request.TokenId);
            if (token == null)
                return null;

            var response = new UserCreateResponse();

            response.Id = _facade.Create(request.User);

            return response;

        }

        [HttpPost("update")]
        public UserUpdateResponse Update([FromBody]UserUpdateRequest request)
        {
            var token = _tokenFacade.GetWithTokenId(request.TokenId);
            if (token == null)
                return null;
            var response = new UserUpdateResponse();
            _facade.Update(request.User);

            return response;

        }

        [HttpPost("search")]
        public UserSearchResponse Search(UserSearchRequest request)
        {
            var token = _tokenFacade.GetWithTokenId(request.TokenId);
            if (token == null)
                return null;

            var response = new UserSearchResponse();

            response.List = _facade.Search(request.Filter, request.PaginationInfo);

            return response;
        }


        [HttpPost("detail")]
        public UserGetResponse Get([FromBody]UserGetRequest request)
        {
            var token = _tokenFacade.GetWithTokenId(request.TokenId);
            if (token == null)
                return null;

            var response = new UserGetResponse();

            response.User = _facade.Get(request.Id);

            return response;
        }

        [HttpPost("delete")]

        public UserDeleteResponse Delete([FromBody]UserDeleteRequest request)
        {
            var token = _tokenFacade.GetWithTokenId(request.TokenId);
            if (token == null)
                return null;

            var response = new UserDeleteResponse();
            _facade.Delete(request.Id);
            return response;

        }





    }
}