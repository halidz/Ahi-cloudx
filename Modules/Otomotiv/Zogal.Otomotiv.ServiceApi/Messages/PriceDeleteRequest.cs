using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zogal.Core;

namespace Zogal.Otomotiv.ServiceApi.Messages
{
    public class PriceDeleteRequest :ISecureRequest
    {
        public long Id { get; set; }
        public Guid TokenId { get ; set; }
    }
}
