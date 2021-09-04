﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Domain.IdentityModels
{
    public static class UserRoles
    {
        public const string Admin = "admin";
        public const string User = "user";
        public const string AdminUser = Admin + "," + User;
    }
}