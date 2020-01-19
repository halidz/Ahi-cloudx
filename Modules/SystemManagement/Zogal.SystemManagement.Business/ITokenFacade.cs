using System;
using Zogal.SystemManagement.ViewModel;

namespace Zogal.SystemManagement.Business
{
    public interface ITokenFacade
    {
        long Create(LoginView Token);
        TokenView GetWithTokenId(Guid id);
        TokenView Get(long id);
        string GetAuth(long id);
    }
}
