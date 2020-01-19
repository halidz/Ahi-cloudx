using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Otomotiv.EntityModel;

namespace Zogal.Otomotiv.NhMapping
{
    public class OperationMap : ClassMapping<Operation>
    {
        public OperationMap()
        {
            Table("OTO_OPERATION");

            Id(x => x.Id, map =>
              {
                  map.Column("OPERATIONID");
                  map.Generator(Generators.Native);
              });

            Property(x => x.Plate, map => map.Column("PLATE"));
            Property(x => x.FirstName, map => map.Column("FIRSTNAME"));
            Property(x => x.LastName, map => map.Column("LASTNAME"));
            Property(x => x.Gender, map => map.Column("GENDER"));
            Property(x => x.PhoneNumber, map => map.Column("PHONENUMBER"));
            Property(x => x.PaymentMethod, map => map.Column("PAYMENTMETHOD"));
            Property(x => x.OperationDate, map => map.Column("OPERATIONDATE"));
            Property(x => x.PeriodDate, map => map.Column("PERIODDATE"));

            Property(x => x.VehicleType, map => map.Column("VEHICLETYPE"));
            Property(x => x.VehicleModel, map => map.Column("VEHICLEMODEL"));
            Property(x => x.VehicleBrand, map => map.Column("VEHICLEBRAND"));

            Property(x => x.CalculatedAmount, map => map.Column("CALCULATEDAMOUNT"));
            Property(x => x.OperationAmount, map => map.Column("OPEARATIONAMOUNT"));

            Property(x => x.TipAmount, map => map.Column("TIPAMOUNT"));
          

            Property(x => x.OperationTypeId, map => map.Column("OPERATIONTYPEID"));

            Property(x => x.Status, map => map.Column("STATUS"));
            Property(x => x.CreatedBy, map => map.Column("CREATEDBY"));
            Property(x => x.CreatedByFullName, map => map.Column("CREATEDBYFULLNAME"));
            Property(x => x.CreatedDate, map => map.Column("CREATEDDATE"));
            Property(x => x.UpdatedBy, map => map.Column("UPDATEDBY"));
            Property(x => x.UpdatedByFullName, map => map.Column("UPDATEDBYFULLNAME"));
            Property(x => x.UpdatedDate, map => map.Column("UPDATEDDATE"));

            Property(x => x.Description, map => map.Column("DESCRIPTION"));




        }
    }
}
