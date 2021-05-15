using BankApp.Data.Contexts;
using BankApp.Domain.Interfaces;
using BankApp.Domain.Models;
using System.Collections.Generic;

namespace BankApp.Data.Reposetories
{
    public class CustomerRepository : ICustomerRepository
    {
        public BankAppDataContext _context;

        public CustomerRepository(BankAppDataContext context)
        {
            _context = context;
        }

        public void DeleteCustomer(Customer customer)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }

        public Customer GetCustomer(int id)
        {
            return _context.Customers.Find(id);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers;
        }

        public void PostCustomer(Customer customer)
        {
            _context.Add(customer);
            _context.SaveChanges();
        }

        public void PutCustomer(Customer customer)
        {
            _context.Update(customer);
            _context.SaveChanges();
        }
    }
}
