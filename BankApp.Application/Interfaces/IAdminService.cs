﻿using BankApp.Domain.Models;
using BankApp.Enteties.DataTransferObjects;
using BankApp.Enteties.DataTransferObjects.IdentityDTO;
using BankApp.Enteties.Models.RequestFeatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankApp.Application.Interfaces
{
    public interface IAdminService
    {
        void AddAccountType(AccountType accountType);

        List<AccountTypeDTO> GetAccountTypes();

        PagedList<CustomerDTO> GetCustomers(CustomerParameters parameters);

        List<AccountDTO> GetCustomerAccounts(int id);

        void AddLoan(Loan loan);

        void AddNewCustomerProfile(RegisterCustomerDTO customerModel);

        void AddCustomerLogin(RegristrationDTO regristration);
    }
}