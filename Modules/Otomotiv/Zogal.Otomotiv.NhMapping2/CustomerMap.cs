using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Otomotiv.EntityModel;

namespace Zogal.Otomotiv.NhMapping
{
    public class CustomerMap : ClassMapping<Customer>
    {
        public CustomerMap()
        {
            Table("OTO_CUSTOMER");

            Id(x => x.Id, map =>
              {
                  map.Column("CUSTOMERID");
                  map.Generator(Generators.Native);
              });

            Property(x => x.FirstName, map => map.Column("FIRSTNAME"));

            Property(x => x.LastName, map => map.Column("LASTNAME"));

            Property(x => x.PhoneNumber, map => map.Column("PHONENUMBER"));

            Bag(x => x.Subscribers, map =>
             {
                 map.Key(k => k.Column("CUSTOMERID"));

                 map.Cascade(Cascade.None);

                 map.Table("OTO_CUSTOMER_SUBSCRIBER");

                 map.Lazy(CollectionLazy.Lazy);
                
             },

             ce=> ce.ManyToMany(l =>
             {
                 l.Column("SUBSCRIBERID");
                 l.Class(typeof(Subscriber));
             }


            
             ));


        }
    }
}
