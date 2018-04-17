using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CtgModels.DataModels;

namespace CtgModels.Interfaces
{
    public interface IRepository<T, in TKeyType> : IDisposable where T : BaseEntity
    {
        IQueryable<T> Select();
        IEnumerable<TResult> Select<TResult>(Func<T, TResult> func);

        void Insert(T newItem);

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        T FindSingle(Expression<Func<T, bool>> predicate);

        void Delete(T item);

        IEnumerable<T> Top<TResult>(int count, Func<T, TResult> orderBy);

        void Delete(IEnumerable<T> range);
    }
}