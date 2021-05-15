using BankApp.Data.Contexts;
using BankApp.Domain.Interfaces;
using BankApp.Domain.Models;
using System.Collections.Generic;

namespace BankApp.Data.Reposetories
{
    public class CardRepository : ICardRepository
    {
        public BankAppDataContext _context;

        public CardRepository(BankAppDataContext context)
        {
            _context = context;
        }

        public void DeleteCard(Card card)
        {
            _context.Cards.Remove(card);
            _context.SaveChanges();
        }

        public Card GetCard(int id)
        {
            return _context.Cards.Find(id);
        }

        public IEnumerable<Card> GetCards()
        {
            return _context.Cards;
        }

        public void PostCard(Card card)
        {
            _context.Add(card);
            _context.SaveChanges();
        }

        public void PutCard(Card card)
        {
            _context.Update(card);
            _context.SaveChanges();
        }
    }
}
