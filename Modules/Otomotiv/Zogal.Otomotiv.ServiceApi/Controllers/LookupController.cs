using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Zogal.Otomotiv.Business;
using Zogal.Otomotiv.ServiceApi.Messages;

namespace Zogal.Otomotiv.ServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupController : ControllerBase
    {
        public ILookupFacade _lookupFacade;
        public LookupController(ILookupFacade lookupFacade)
        {
            _lookupFacade = lookupFacade ?? throw new ArgumentNullException(nameof(lookupFacade));
        }

        [HttpPost("getLookup")]
        public object GetLookup()
        {
            //string allText = System.IO.File.ReadAllText(@"C:\Ahi-cloud\Modules\Otomotiv\Zogal.Otomotiv.Core\lookups.json");
            string allText = System.IO.File.ReadAllText(@"lookups.json");

            object jsonObject = JsonConvert.DeserializeObject(allText);
            return jsonObject;
        }

        [HttpPost("getLookupWithKey")]
        public object GetLookupWithKey(LookupGetRequest request)
        {

            return _lookupFacade.GetLookup(request.Key);
        }

    }
}