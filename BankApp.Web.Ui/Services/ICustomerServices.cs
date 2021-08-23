using BankApp.Web.Ui.Models;
using System;
using System.Collections.Generic;

namespace BankApp.Web.Ui.Services
{
    public interface ICustomerServices
    {
        public AccountDTO Account
        {
            get;
        }

        CustomerDetails CustomerInfo { get; }
        List<TransferDTO> Transactions { get; }

        event Action OnChange;

        void SelectedAccount(int accountId);

        void GetDetails();

        void GetTransactions();

        void InternalTransaction(InternalTransaction transaction);
    }
}