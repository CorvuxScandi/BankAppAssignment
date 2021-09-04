using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Enteties.DataTransferObjects
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }

        [DisplayName("Kön")]
        public string Gender { get; set; }

        [DisplayName("Förnamn")]
        public string Givenname { get; set; }

        [DisplayName("Efternamn")]
        public string Surname { get; set; }

        [DisplayName("Addres")]
        public string Streetaddress { get; set; }

        [DisplayName("Postort")]
        public string City { get; set; }

        [DisplayName("Postkod")]
        public string Zipcode { get; set; }

        [DisplayName("Land")]
        public string Country { get; set; }

        [DisplayName("Landskod")]
        public string CountryCode { get; set; }

        [DisplayName("Födelsedag")]
        public DateTime? Birthday { get; set; }

        [DisplayName("Telefon landskod")]
        public string Telephonecountrycode { get; set; }

        [DisplayName("Telefonnummer")]
        public string Telephonenumber { get; set; }

        [DisplayName("Email addres")]
        public string Emailaddress { get; set; }
    }
}