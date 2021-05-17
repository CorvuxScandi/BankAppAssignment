using BankApp.Data.Contexts;
using BankApp.Domain.Interfaces;
using BankApp.Domain.Models;
using System.Collections.Generic;

namespace BankApp.Data.Reposetories
{
    public class CardRepository : IRepository<Card>
    {
        public BankAppDataContext _context;

        public CardRepository(BankAppDataContext context)
        {
            _context = context;
        }

        public void Create(Card entity)
        {
            _context.Add(entity);
        }

        public void Delete(Card entity)
        {
            _context.Remove(entity);
        }

        public IEnumerable<Card> GetAll()
        {
            return _context.Cards;
        }

        public Card GetById(int id)
        {
            return _context.Cards.Find(id);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Update(Card entity)
        {
            _context.Update(entity);
        }
    }
}