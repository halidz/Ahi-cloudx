﻿using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Zogal.SystemManagement.EntityModel;

namespace Zogal.SystemManagement.NhMapping
{
    public class RoleMap : ClassMapping<Role>
    {
        public RoleMap()
        {
            Table("SYM_ROLE");
            Id(x => x.Id,
                map =>
                {
                    map.Column("ROLEID");
                    map.Generator(Generators.Native);

                });
            Property(x => x.Name, map =>map.Column("NAME"));
            Property(x => x.Code, map => map.Column("CODE"));
        }
    }
}
