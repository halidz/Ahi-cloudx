﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zogal.SystemManagement.ViewModel;

namespace Zogal.SystemManagement.ServiceApi.Messages
{
    public class AuthLoginRequest
    {
        public LoginView Login { get; set; }
    }
}
