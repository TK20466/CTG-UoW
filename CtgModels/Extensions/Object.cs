using System;

namespace CtgModels.Extensions
{
    public static class Object
    {
        public static T Map<T, T2>(this T2 obj, Func<T2, T> mapper)
        {
            return mapper(obj);
        }
    }
}