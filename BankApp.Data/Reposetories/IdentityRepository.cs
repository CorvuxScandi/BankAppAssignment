using BankApp.Data.Contexts;
using BankApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Data.Reposetories
{
    internal class IdentityRepository : IRepository<ApplicationUser>
    {
        private IdentityDbContext _context;

        public IdentityRepository(IdentityDbContext context)
        {
            _context = context;
        }

        public void Create(ApplicationUser entity)
        {
            _context.Add(entity);
        }

        public void Delete(ApplicationUser entity)
        {
            _context.Remove(entity);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _context.Users;
        }

        public ApplicationUser GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Update(ApplicationUser entity)
        {
            _context.Update(entity);
        }
    }
}