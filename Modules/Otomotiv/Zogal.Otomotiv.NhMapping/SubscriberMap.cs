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

            Property(x => x.CustomerId, map => map.Column("CUSTOMERID"));

            Property(x => x.StartDate, map => map.Column("STARTDATE"));
            Property(x => x.LastStartDate, map => map.Column("LASTSTARTDATE"));

            Property(x => x.Status, map => map.Column("STATUS"));

            Property(x => x.MonthlySubscription, map => map.Column("MONTHLYSUBSCRIPTION"));

            Property(x => x.Plate, map => map.Column("PLATE"));

            Property(x => x.Brand, map => map.Column("BRAND"));

            Property(x => x.Model, map => map.Column("MODEL"));

            Property(x => x.Type, map => map.Column("TYPE"));

            Property(x => x.Deposit, map => map.Column("DEPOSIT"));

            Property(x => x.CreatedBy, map => map.Column("CREATEDBY"));
            Property(x => x.CreatedByFullName, map => map.Column("CREATEDBYFULLNAME"));
            Property(x => x.CreatedDate, map => map.Column("CREATEDDATE"));
            Property(x => x.UpdatedBy, map => map.Column("UPDATEDBY"));
            Property(x => x.UpdatedByFullName, map => map.Column("UPDATEDBYFULLNAME"));
            Property(x => x.UpdatedDate, map => map.Column("UPDATEDDATE"));
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
