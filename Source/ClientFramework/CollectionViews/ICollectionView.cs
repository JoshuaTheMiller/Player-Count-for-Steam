using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Trfc.ClientFramework.CollectionViews
{
    public interface ICollectionView<T> : IEnumerable<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        IEnumerable<T> Source { get; }

        int Count { get; }

        IEnumerable<Predicate<T>> Filters { get; }

        IEqualityComparer<T> ItemComparer { get; }        

        Func<IEnumerable<T>, IEnumerable<T>> OrderingFunction { get; }        

        void SyncNewSourceItems(IEnumerable<T> newList);

        void Refresh();
    }
}
