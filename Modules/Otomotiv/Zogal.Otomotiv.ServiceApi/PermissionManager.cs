using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zogal.Otomotiv.ServiceApi
{
    public class PermissionManager
    {
     

        public PermissionManager()
        {
           
        }

        public bool AuthorizationControl(Guid TokenId)
        {         
            if (TokenId == null)
                return false;
            return true;
        }



    }
}
