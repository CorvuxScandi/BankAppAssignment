﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BankApp.Web.Ui.Models
{
    public class RegisterModel
    {
        //Customer
        public string Gender { get; set; }
        [Display(Name = "First name")]
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string Streetaddress { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public DateTime Birthday { get; set; }
        public string Telephonecountrycode { get; set; }
        public string Telephonenumber { get; set; }
        public string Emailaddress { get; set; }

        //Account
        public string Frequency { get; set; }
        public int AccountTypesId { get; set; }
        public DateTime Created { get; set; }
        public decimal Balance { get; set; }
    }
}