using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Otomotiv.EntityModel;

namespace Zogal.Otomotiv.NhMapping
{
    public class PriceMap : ClassMapping<Price>
    {
        public PriceMap()
        {
            Table("OTO_PRICEMAP");
            Id(x => x.Id, map => {
                map.Column("ID");
                map.Generator(Generators.Native);
            });

            Property(x => x.OperationTypeId, map => map.Column("OPERATIONTYPEID"));
            Property(x => x.VehicleType, map => map.Column("VEHICLETYPE"));
            Property(x => x.DefaultPrice, map => map.Column("DEFAULTPRICE"));
            Property(x => x.Status, map => map.Column("STATUS"));

        }
    }
}
