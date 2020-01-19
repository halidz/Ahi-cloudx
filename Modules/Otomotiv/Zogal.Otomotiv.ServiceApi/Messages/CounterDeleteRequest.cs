using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zogal.Core;

namespace Zogal.Otomotiv.ServiceApi.Messages
{
    public class CounterDeleteRequest : ISecureRequest

    {
        public long Id { get; set; }
        public Guid TokenId { get ; set; }
    }
}
