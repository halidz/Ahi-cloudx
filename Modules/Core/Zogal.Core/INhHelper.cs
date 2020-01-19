using NHibernate;
namespace Zogal.Core
{
    public interface INhHelper
    {
        ISession Session { get; }
    }
}
