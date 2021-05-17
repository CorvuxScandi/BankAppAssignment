using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Create(T entity);
        #region Read
        IEnumerable<T> GetAll();
        T GetById(int id);
        #endregion
        void Update(T entity);
        void Delete(T entity);
        int Save();

    }
}
