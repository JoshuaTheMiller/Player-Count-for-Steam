using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Trfc.ClientFramework.CollectionViews
{
    public interface ICollectionView<T> : IEnumerable<T>, IRefreshable, INotifyCollectionChanged, INotifyPropertyChanged
    {
        IEnumerable<T> Source { get; }

        int Count { get; }

        IEnumerable<Predicate<T>> Filters { get; }

        IEqualityComparer<T> ItemComparer { get; }

        Func<IEnumerable<T>, IEnumerable<T>> OrderingFunction { get; }        

        Task SyncNewSourceItemsAsync(IEnumerable<T> newList);        
    }
}
