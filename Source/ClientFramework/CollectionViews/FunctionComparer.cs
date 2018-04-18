using System;
using System.Collections.Generic;

namespace Trfc.ClientFramework.CollectionViews
{
    internal sealed class FunctionComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> comparerFunction;

        public FunctionComparer(Func<T, T, bool> comparer)
        {
            this.comparerFunction = comparer;
        }

        public bool Equals(T x, T y)
        {
            return comparerFunction.Invoke(x, y);
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }
}
