using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Otomotiv.EntityModel;

namespace Zogal.Otomotiv.NhMapping
{
    public class VehicleTypeMap : ClassMapping<VehicleType>
    {
        public VehicleTypeMap()
        {
            Table("OTO_VEHICLETYPE");

            Id(x => x.Id, map =>
            {
                map.Column("ID");
                map.Generator(Generators.Native);

            });

            Property(x => x.Name, map => map.Column("NAME"));

            Property(x => x.DefaultPrice, map => map.Column("DEFAULTPRİCE"));
        }
    }
}
