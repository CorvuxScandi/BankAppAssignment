using BankApp.Data.Contexts;
using BankApp.Domain.Interfaces;
using BankApp.Domain.Models;
using System.Collections.Generic;

namespace BankApp.Data.Reposetories
{
    public class CustomerRepository : IRepository<Customer>
    {
        public BankAppDataContext _context;

        public CustomerRepository(BankAppDataContext context)
        {
            _context = context;
        }

        public void Create(Customer entity)
        {
            _context.Add(entity);
        }

        public void Delete(Customer entity)
        {
            _context.Remove(entity);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers;
        }

        public Customer GetById(int id)
        {
            return _context.Customers.Find(id);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Update(Customer entity)
        {
            _context.Update(entity);
        }
    }
}