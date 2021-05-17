using BankApp.Data.Contexts;
using BankApp.Domain.Interfaces;
using BankApp.Domain.Models;
using System.Collections.Generic;

namespace BankApp.Data.Reposetories
{
    public class DispositionRepository : IRepository<Disposition>
    {
        public BankAppDataContext _context;

        public DispositionRepository(BankAppDataContext context)
        {
            _context = context;
        }

        public void Create(Disposition entity)
        {
            _context.Add(entity);
        }

        public void Delete(Disposition entity)
        {
            _context.Remove(entity);
        }

        public IEnumerable<Disposition> GetAll()
        {
            return _context.Dispositions;
        }

        public Disposition GetById(int id)
        {
            return _context.Dispositions.Find(id);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Update(Disposition entity)
        {
            _context.Update(entity);
        }
    }
}