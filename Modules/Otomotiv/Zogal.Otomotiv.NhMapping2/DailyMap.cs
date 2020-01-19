using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Otomotiv.EntityModel;

namespace Zogal.Otomotiv.NhMapping
{
    public class DailyMap :ClassMapping<Daily>
    {
        public DailyMap()
        {
            Table("OTO_DAILY");

            Id(x => x.Id, map =>
              {
                  map.Column("DAILYID");
                  map.Generator(Generators.Native);
              }

            );

            Property(x => x.Plate, map => map.Column("PLATE"));

            Property(x => x.PaymentMethod, map => map.Column("PAYMENTMETHOD"));

            Property(x => x.Code, map => map.Column("OPERATIONCODE"));

            Property(x => x.Date, map => map.Column("DATE"));

            Property(x => x.Amount, map => map.Column("AMOUNT"));

            Property(x => x.OtherAmount, map => map.Column("OTHERAMOUNT"));

            Property(x => x.TotalAmount, map => map.Column("TOTALAMOUNT"));








        }
    }
}
