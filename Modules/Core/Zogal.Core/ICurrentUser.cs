﻿namespace Zogal.Core
{
    public interface ICurrentUser
    {
       string UserName { get; set; }

       string FullName { get; set; }
    }
}
