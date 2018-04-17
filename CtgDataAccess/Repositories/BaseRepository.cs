using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CtgDataAccess.db;
using CtgModels.DataModels;
using CtgModels.Interfaces;

namespace CtgDataAccess.Repositories
{
    public abstract class BaseRepository
    {
        public readonly DataContext _context;

        protected BaseRepository()
        {
            _context = new DataContext();
        }

        protected BaseRepository(DataContext context)
        {
            _context = context;
        }

        public void Dispose()
        {

        }
    }

    public abstract class Repository<T, TKey> : BaseRepository, IRepository<T, TKey> where T : BaseEntity
    {

        protected Repository() : base() { }

        protected Repository(DataContext context) : base(context)
        { }

        public virtual IQueryable<T> Select()
        {
            return _context.Set<T>();
        }

        public virtual IEnumerable<TResult> Select<TResult>(Func<T, TResult> func)
        {
            return _context.Set<T>().Select(func);
        }

        public abstract T FetchByKey(TKey value);

        public virtual void Insert(T newItem)
        {
            _context.Set<T>().Add(newItem);
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public virtual T FindSingle(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).FirstOrDefault();
        }

        public virtual void Delete(T item)
        {
            _context.Set<T>().Remove(item);
        }

        public IEnumerable<T> Top<TResult>(int count, Func<T, TResult> orderBy)
        {
            return _context.Set<T>().OrderByDescending(orderBy).Take(count);
        }

        public void Delete(IEnumerable<T> range)
        {
            _context.Set<T>().RemoveRange(range);
        }
    }
}
