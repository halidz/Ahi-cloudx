using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Otomotiv.EntityModel;

namespace Zogal.Otomotiv.NhMapping
{
    public class OperationTypeMap : ClassMapping<OperationType>
    {
        public OperationTypeMap()
        {
            Table("OTO_OPERATIONTYPE");

            Id(x => x.Id, map =>
              {
                  map.Column("OPERATIONTYPEID");
                  map.Generator(Generators.Native);
              });
            Property(x => x.Name, map => map.Column("NAME"));
            Property(x => x.Code, map => map.Column("CODE"));
            Property(x => x.AccountType, map => map.Column("ACCOUNTTYPE"));
        }
    }
}
