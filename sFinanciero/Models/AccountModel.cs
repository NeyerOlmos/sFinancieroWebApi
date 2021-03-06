﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sFinanciero.Models
{
    public class AccountModel
    {
        public string  Id { get; set; }
        public string  UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string LoggedOn { get; set; }
        public string[] Roles { get; set; }
    }
}