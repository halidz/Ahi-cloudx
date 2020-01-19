using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Otomotiv.EntityModel;

namespace Zogal.Otomotiv.NhMapping
{
    public class TipMap : ClassMapping<Tip>
    {
        public TipMap()
        {
            Table("OTO_TIP");

            Id(x => x.Id, map =>
               {
                   map.Column("ID");
                   map.Generator(Generators.Native);
               });
            Property(x => x.OperationDate, map => map.Column("OPERATIONDATE"));
            Property(x => x.PeriodDate, map => map.Column("PERIODDATE"));
            Property(x => x.TipAmount, map => map.Column("TIPAMOUNT"));
            Property(x => x.WorkerNumber, map => map.Column("WORKERNUMBER"));
            Property(x => x.Description, map => map.Column("DESCRIPTION"));
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
