using System;
using System.Collections.Generic;
using System.Linq;

namespace Trfc.ClientFramework.CollectionViews
{
    public sealed class CollectionView<T> : RangedObservableCollection<T>, ICollectionView<T>
    {
        private IList<T> source;

        new public int Count => source.Count;

        public IEnumerable<T> Source => source;

        public IEnumerable<Predicate<T>> Filters { get; }

        public IEqualityComparer<T> ItemComparer { get; }

        public Func<IEnumerable<T>, IEnumerable<T>> OrderingFunction { get; }

        internal CollectionView(IEnumerable<T> source,
            IEnumerable<Predicate<T>> filters,
            IEqualityComparer<T> itemComparer,
            Func<IEnumerable<T>, IEnumerable<T>> orderingFunction)
            : base(source)
        {
            this.source = source.ToList();
            this.Filters = filters;
            this.ItemComparer = itemComparer;
            this.OrderingFunction = orderingFunction;
        }

        public void SyncNewSourceItems(IEnumerable<T> newList)
        {
            var sourceDoesNotNeedChanging = source.SequenceEqual(newList, ItemComparer);

            if (sourceDoesNotNeedChanging)
            {
                return;
            }

            source = newList.ToList();

            Refresh();
        }

        public void Refresh()
        {
            var sourceWithFiltersApplied = source
                .Where(ItemPassesFilters).ToList();

            if (OrderingFunction != null)
            {
                var orderedRange = OrderingFunction.Invoke(sourceWithFiltersApplied);

                this.ReplaceWithRange(orderedRange);
            }
            else
            {
                var itemsToRemove = Items
                    .Except(sourceWithFiltersApplied, ItemComparer).ToList();

                PerformActionOnItems(itemsToRemove, Remove, RemoveRange);

                var itemsToAdd = sourceWithFiltersApplied
                    .Except(Items, ItemComparer).ToList();

                PerformActionOnItems(itemsToAdd, Add, AddRange);
            }
        }

        new private void Remove(T itemToRemove)
        {
            base.Remove(itemToRemove);
        }

        private void PerformActionOnItems(IList<T> items, Action<T> actionToPerformOnSmallChange, Action<IEnumerable<T>> actionToPerformOnLargeChange)
        {
            //If it's a large change, do a complete list reset.
            //Large is just a random number...
            if (items.Count > 10)
            {
                actionToPerformOnLargeChange(items);
                return;
            }

            //If it's a small change, add items individually.
            foreach (var missingItem in items)
            {
                actionToPerformOnSmallChange(missingItem);
            }
        }

        private bool ItemPassesFilters(T item)
        {
            foreach (var filter in Filters)
            {
                if (!filter.Invoke(item))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
