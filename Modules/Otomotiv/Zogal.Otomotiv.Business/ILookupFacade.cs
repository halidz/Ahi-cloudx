using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Otomotiv.ViewModel;

namespace Zogal.Otomotiv.Business
{
    public interface ILookupFacade
    {
        List<JsonView> GetLookup(string key);
    }
}
