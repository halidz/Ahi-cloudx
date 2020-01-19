using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Otomotiv.EntityModel;

namespace Zogal.Otomotiv.NhMapping
{
    public class SubscriberMap :ClassMapping<Subscriber>
    {
        public SubscriberMap()
        {
            Table("OTO_SUBSCRIBER");

            Id(x => x.Id, map =>
             {
                 map.Column("SUBSCRIBERID");

                 map.Generator(Generators.Native);

             });


            Property(x => x.StartDate, map => map.Column("STARTDATE"));

            Property(x => x.Status, map => map.Column("STATUS"));

            OneToOne(x=> x.Vehicle, map => map.Cascade(Cascade.All));

            //OneToOne(x => x.Vehicle, m =>
            //{
            //    m.Cascade(Cascade.Persist);
            //    m.Constrained(true);
            //    m.Lazy(LazyRelation.Proxy); // or .NoProxy, .NoLazy

            //    m.Access(Accessor.Field);
            //});


        }
    }
}
