using BankApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();

        Customer GetCustomer(int id);

        void PostCustomer(Customer customer);

        void PutCustomer(Customer customer);

        void DeleteCustomer(Customer customer);
    }
}
