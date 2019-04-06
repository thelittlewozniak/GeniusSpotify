using RepositoryDatabase.Database;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RepositoryDatabase
{
    public class RepositoryDB<T> : IRepository<T> where T : EntityBase
    {
        private DbContext _historyDb;
        public RepositoryDB(DbContext historyDb)
        {
            _historyDb = historyDb;
        }
        public void Add(T entity)
        {
            _historyDb.Set<T>().Add(entity);
            _historyDb.SaveChanges();
        }

        public void Delete(T entity)
        {
            _historyDb.Set<T>().Remove(entity);
            _historyDb.SaveChanges();
        }

        public void Edit(T entity)
        {
            _historyDb.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public T GetById(int id)
        {
            return _historyDb.Set<T>().Find(id);
        }

        public IEnumerable<T> List()
        {
            return _historyDb.Set<T>().AsEnumerable();
        }
    }
}
