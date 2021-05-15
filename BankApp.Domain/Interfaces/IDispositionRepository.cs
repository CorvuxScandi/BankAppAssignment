using BankApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Domain.Interfaces
{
    public interface IDispositionRepository
    {
        IEnumerable<Disposition> GetDispositions();

        Disposition GetDisposition(int id);

        void PostDisposition(Disposition disposition);

        void PutDisposition(Disposition disposition);

        void DeleteDisposition(Disposition disposition);
    }
}
