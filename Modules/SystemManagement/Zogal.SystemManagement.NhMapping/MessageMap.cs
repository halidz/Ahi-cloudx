using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Zogal.SystemManagement.EntityModel;

namespace Zogal.SystemManagement.NhMapping
{
    public class MessageMap : ClassMapping<Message>
    {
        public MessageMap()
        {
            Table("SYM_MESSAGE");
            Id(x => x.Id, map =>
            {
                map.Column("MESSAGEID");
                map.Generator(Generators.Native);
            });
            Property(x => x.Source, map => map.Column("SOURCE"));
            Property(x => x.Name, map => map.Column("NAME"));
            Property(x => x.Email, map => map.Column("EMAIL"));
            Property(x => x.PhoneNumber, map => map.Column("PHONENUMBER"));
            Property(x => x.Subject, map => map.Column("SUBJECT"));
            Property(x => x.Body, map => map.Column("BODY"));
        }
    }
}
