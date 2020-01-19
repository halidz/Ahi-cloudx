using Zogal.Core;

namespace Zogal.SystemManagement.EntityModel
{
    public class Message:IEntity
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string Subject { get; set; }
        public virtual string Body { get; set; }
        public virtual string Source { get; set; }
    }
}
