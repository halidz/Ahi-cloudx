using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Otomotiv.EntityModel;

namespace Zogal.Otomotiv.NhMapping
{
    public class VehicleMap :ClassMapping<Vehicle>
    {
        public VehicleMap()
        {
            Table("OTO_VEHICLE");

            Id(x => x.Id, map =>
             {
                 map.Column("VEHICLEID");
                 map.Generator(Generators.Native);

             });

            Property(x => x.Model, map => map.Column("MODEL"));

            Property(x => x.Plate, map => map.Column("PLATE"));

            Property(x => x.Type, map => map.Column("TYPE"));

            
        }
    }
}
