using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Otomotiv.EntityModel;

namespace Zogal.Otomotiv.NhMapping
{
    public class CounterMap : ClassMapping<Counter>
    {
        public CounterMap()
        {
            Table("OTO_COUNTER");

            Id(x => x.Id, map =>
            {
                map.Column("ID");
                map.Generator(Generators.Native);
            });

            Property(x => x.Date, map => map.Column("DATE"));

            Property(x => x.Type, map => map.Column("TYPE"));

            Property(x => x.StartValue, map => map.Column("STARTVALUE"));

            Property(x => x.StopValue, map => map.Column("STOPVALUE"));

            Property(x => x.SpentValue, map => map.Column("SPENTVALUE"));

            Property(x => x.Status, map => map.Column("STATUS"));
            Property(x => x.CreatedBy, map => map.Column("CREATEDBY"));
            Property(x => x.CreatedByFullName, map => map.Column("CREATEDBYFULLNAME"));
            Property(x => x.CreatedDate, map => map.Column("CREATEDDATE"));
            Property(x => x.UpdatedBy, map => map.Column("UPDATEDBY"));
            Property(x => x.UpdatedByFullName, map => map.Column("UPDATEDBYFULLNAME"));
            Property(x => x.UpdatedDate, map => map.Column("UPDATEDDATE"));
        }
    }
}
