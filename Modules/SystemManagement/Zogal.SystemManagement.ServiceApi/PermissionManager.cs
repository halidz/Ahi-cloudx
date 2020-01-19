using System;
using Zogal.SystemManagement.Business;

namespace Zogal.SystemManagement.ServiceApi
{
    public class PermissionManager
    {
        ITokenFacade _tokenFacade;

        public PermissionManager(ITokenFacade tokenFacade)
        {
            _tokenFacade = tokenFacade ?? throw new ArgumentNullException(nameof(tokenFacade));
        }
        public bool AuthorizationControl(Guid TokenId)
        {
            var token = _tokenFacade.GetWithTokenId(TokenId);
            if (token == null) return false;
            return true;
        }
    }
}