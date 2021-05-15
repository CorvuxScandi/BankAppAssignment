using BankApp.Data.Contexts;
using BankApp.Domain.Interfaces;
using BankApp.Domain.Models;
using System.Collections.Generic;

namespace BankApp.Data.Reposetories
{
    public class DispositionRepository : IDispositionRepository
    {
        public BankAppDataContext _context;

        public DispositionRepository(BankAppDataContext context)
        {
            _context = context;
        }

        public void DeleteDisposition(Disposition disposition)
        {
            _context.Dispositions.Remove(disposition);
            _context.SaveChanges();
        }

        public Disposition GetDisposition(int id)
        {
            return _context.Dispositions.Find(id);
        }

        public IEnumerable<Disposition> GetDispositions()
        {
            return _context.Dispositions;
        }

        public void PostDisposition(Disposition disposition)
        {
            _context.Add(disposition);
            _context.SaveChanges();
        }

        public void PutDisposition(Disposition disposition)
        {
            _context.Update(disposition);
            _context.SaveChanges();
        }
    }
}
