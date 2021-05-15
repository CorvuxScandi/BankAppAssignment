using BankApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Domain.Interfaces
{
    public interface ICardRepository
    {
        IEnumerable<Card> GetCards();

        Card GetCard(int id);

        void PostCard(Card card);

        void PutCard(Card card);

        void DeleteCard(Card card);

    }
}
