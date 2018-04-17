using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtgModels.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class IEnumerable
    {

        public static IEnumerable<T> MakeConcrete<T>(this IQueryable<T> queryable)
        {
            return queryable.ToList();
        }

        public static IEnumerable<T> Execute<T>(this IQueryable<T> queryable)
        {
            return queryable.MakeConcrete();
        }

        public static IEnumerable<T> Top<T, TKey>(this IEnumerable<T> enumerable, int count, Func<T, TKey> orderBy)
        {
            return enumerable.OrderByDescending(orderBy).Take(count);
        }

    }
}
