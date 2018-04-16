using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trfc.ClientFramework.CollectionViews
{
    public sealed class CollectionView<T> : RangedObservableCollection<T>, ICollectionView<T>
    {
        private object someLock = new object();

        private IList<T> source;

        new public int Count => source.Count;

        public IEnumerable<T> Source => source;

        public IEnumerable<Predicate<T>> Filters { get; }

        public IEqualityComparer<T> ItemComparer { get; }

        public Func<IEnumerable<T>, IEnumerable<T>> OrderingFunction { get; }

        public bool IsRefreshing { get; private set; }

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

        public async Task SyncNewSourceItemsAsync(IEnumerable<T> newList)
        {
            lock (someLock)
            {
                var sourceDoesNotNeedChanging = source.SequenceEqual(newList, ItemComparer);

                if (sourceDoesNotNeedChanging)
                {
                    return;
                }
            }

            source = newList.ToList();

            await Refresh();
        }

        public Task Refresh()
        {
            lock (someLock)
            {
                IsRefreshing = true;
            }

            var sourceWithFiltersApplied = source
                .Where(ItemPassesFilters).ToList();

            var itemsToRemove = Items
                .Except(sourceWithFiltersApplied, ItemComparer).ToList();

            PerformActionOnItems(itemsToRemove, Remove, RemoveRange);

            var itemsToAdd = sourceWithFiltersApplied
                .Except(Items, ItemComparer).ToList();

            PerformActionOnItems(itemsToAdd, Add, AddRange);

            //This kind of defeats the attempts above at trying to be 
            //efficient with notifing of list changes as calling ReplaceWithRange
            //calls for an entire reset notification.

            //TODO: add the ability to disable notification of changes...
            //At this point, this refresh function could average 3 notifications of a full reset.
            if (OrderingFunction != null)
            {
                var orderedRange = OrderingFunction.Invoke(Items);

                this.ReplaceWithRange(orderedRange);
            }

            IsRefreshing = false;

            return Task.FromResult(default(object));
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
