using System.Collections.Generic;

namespace RepositoryDatabase
{
    public interface IRepository<T>
    {
        T GetById(int id);
        IEnumerable<T> List();
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
    }
}
