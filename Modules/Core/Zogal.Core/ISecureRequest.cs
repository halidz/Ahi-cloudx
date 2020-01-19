using System;

namespace Zogal.Core
{
    public interface ISecureRequest
    {
         Guid TokenId { get; set; }
    }
}
