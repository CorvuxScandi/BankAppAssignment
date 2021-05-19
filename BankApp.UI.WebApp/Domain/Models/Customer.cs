using System;
using System.Collections.Generic;
using BankApp.Domain.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace BankApp.Domain.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Dispositions = new HashSet<Disposition>();
        }

        public int CustomerId { get; set; }
        public string Gender { get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string Streetaddress { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public DateTime? Birthday { get; set; }
        public string Telephonecountrycode { get; set; }
        public string Telephonenumber { get; set; }
        public string Emailaddress { get; set; }
        public string ApplicationUserId { get; set; }

        public virtual ICollection<Disposition> Dispositions { get; set; }
    }
}
