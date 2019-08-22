using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolent.ContactsMgmt.Common.Contracts
{
    public interface IRepository<T> where T : class
    {
        bool IsDataAlreadyExist(T entity);
        T Get(int id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        void Delete(string value);
        int Save();
    }
}
