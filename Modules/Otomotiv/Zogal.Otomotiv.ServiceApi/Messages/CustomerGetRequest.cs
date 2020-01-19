using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zogal.Core;

namespace Zogal.Otomotiv.ServiceApi.Messages
{
    public class CustomerGetRequest : ISecureRequest
    {
        public Guid TokenId { get; set; }
        public long Id { get; set; }

    }
}
