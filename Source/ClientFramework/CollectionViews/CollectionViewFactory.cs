using System;
using System.Collections.Generic;
using System.Linq;

namespace Trfc.ClientFramework.CollectionViews
{
    public static class CollectionViewFactory
    {
        public static ICollectionView<T> Create<T>(IEnumerable<T> source, IEnumerable<Predicate<T>> filters, Func<T, T, bool> comparerFunction, Func<IEnumerable<T>, IEnumerable<T>> orderingFunction, SyncParameters<T> syncParameters)
        {
            var functionComparer = new FunctionComparer<T>(comparerFunction);

            return new CollectionView<T>(source, filters, functionComparer, orderingFunction, syncParameters);
        }

        public static ICollectionView<T> Create<T>(IEnumerable<T> source, IEnumerable<Predicate<T>> filters, Func<T, T, bool> comparerFunction, Func<IEnumerable<T>, IEnumerable<T>> orderingFunction)
        {
            var functionComparer = new FunctionComparer<T>(comparerFunction);

            return new CollectionView<T>(source, filters, functionComparer, orderingFunction, SyncParameters.WithDefaultKeySelector<T>());
        }

        public static ICollectionView<T> Create<T>(IEnumerable<T> source, IEnumerable<Predicate<T>> filters, Func<T, T, bool> comparerFunction)
        {            
            return Create<T>(source, filters, comparerFunction, null);
        }

        public static ICollectionView<T> Create<T>(IEnumerable<T> source, IEnumerable<Predicate<T>> filters)
        {
            return Create<T>(source, filters, DefaultFunction);
        }

        public static ICollectionView<T> Create<T>(IEnumerable<T> source)
        {
            return Create<T>(source, Enumerable.Empty<Predicate<T>>());
        }

        private static bool DefaultFunction<T>(T x, T y)
        {
            return x.Equals(y);
        }
    }
}
