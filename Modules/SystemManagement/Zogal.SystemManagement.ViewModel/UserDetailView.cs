﻿using System.Collections.Generic;
using Zogal.SystemManagement.Core;

namespace Zogal.SystemManagement.ViewModel
{
    public class UserDetailView
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public UserStatus Status { get; set; }
        public IList<RoleItemView> Roles { get; set; }
    }
}
