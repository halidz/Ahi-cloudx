using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Otomotiv.EntityModel;

namespace Zogal.Otomotiv.NhMapping
{
    public class WashTypeMap : ClassMapping<WashType>
    {
        public WashTypeMap()
        {
            Table("OTO_WASHTYPE");

            Id(x => x.Id, map =>
              {
                  map.Column("ID");
                  map.Generator(Generators.Native);
              });

            Property(x => x.Name, map => map.Column("NAME"));
            Property(x => x.DefaultPrice, map => map.Column("DEFAULTPRİCE"));
            Property(x => x.ProcessDuration, map => map.Column("PROCESSDURATION"));
        }
    }
}
