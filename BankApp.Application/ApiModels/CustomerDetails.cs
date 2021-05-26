using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Application.ApiModels
{
    public class CustomerDetails
    {
        public CustomerDTO CustomerInfo { get; set; }
        public List<AccountDTO> Accounts { get; set; }
        public List<CardDTO> Cards { get; set; }
        public List<LoanDTO> Loans { get; set; }
    }

    public class CustomerDTO
    {
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
    }

    public class AccountDTO
    {
        public int AccountId { get; set; }
        public string Frequency { get; set; }
        public DateTime Created { get; set; }
        public decimal Balance { get; set; }
        public string AccountType { get; set; }
    }

    public class CardDTO
    {
        public int CardId { get; set; }
        public string Type { get; set; }
        public DateTime Issued { get; set; }
        public string Cctype { get; set; }
        public string Ccnumber { get; set; }
        public string Cvv2 { get; set; }
        public int ExpM { get; set; }
        public int ExpY { get; set; }
    }

    public class LoanDTO
    {
        public int AccountId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int Duration { get; set; }
        public decimal Payments { get; set; }
        public string Status { get; set; }
    }

    public class TransferDTO
    {
        public int AccountId { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Operation { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string Bank { get; set; }
        public string Account { get; set; }
    }
}