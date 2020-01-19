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
    public class AuthController : ControllerBase
    {
        private ITokenFacade _tokenFacade;
        public AuthController(ITokenFacade tokenFacade)
        {
            _tokenFacade = tokenFacade ?? throw new ArgumentNullException(nameof(tokenFacade));
        }


        [HttpPost("login")]

        public AuthLoginResponse Login(AuthLoginRequest request)
        {
            
            var response = new AuthLoginResponse();
            var id=_tokenFacade.Create(request.Login);
            if (id == -1)
                return null;

            var token = _tokenFacade.Get(id);
            response.TokenId = token.TokenId;
            response.Code = _tokenFacade.GetAuth(id);
            response.ExpireDate = token.ExpireDate;
            return response;

        }


    }
}