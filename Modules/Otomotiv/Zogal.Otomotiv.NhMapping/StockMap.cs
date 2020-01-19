using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Otomotiv.EntityModel;

namespace Zogal.Otomotiv.NhMapping
{
    public class StockMap : ClassMapping<StockItem>
    {
        public StockMap()
        {
            Table("OTO_STOCK");
            Id(x => x.Id, map =>
            {
                map.Column("ID");
                map.Generator(Generators.Native);
            });
            Property(x => x.BarcodeNumber, map => map.Column("BARCODENUMBER"));
            Property(x => x.Name, map => map.Column("NAME"));
            Property(x => x.UnitPrice, map => map.Column("UNITPRICE"));
            Property(x => x.NumberOfUnit, map => map.Column("NUMBEROFUNIT"));
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
