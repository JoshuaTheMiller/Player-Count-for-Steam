using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
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

        public SyncParameters<T> SyncParameters { get; }

        internal CollectionView(IEnumerable<T> source,
            IEnumerable<Predicate<T>> filters,
            IEqualityComparer<T> itemComparer,
            Func<IEnumerable<T>, IEnumerable<T>> orderingFunction,
            SyncParameters<T> syncParameters)
            : base(source)
        {
            this.source = source.ToList();
            this.Filters = filters;
            this.ItemComparer = itemComparer;
            this.OrderingFunction = orderingFunction;
            this.SyncParameters = syncParameters;
        }

        public void SyncNewSourceItems(IEnumerable<T> newList)
        {
            var sourceDoesNotNeedChanging = source.SequenceEqual(newList, ItemComparer);

            if (sourceDoesNotNeedChanging)
            {
                return;
            }

            if (this.SyncParameters.HasDefaultKeySelector)
            {
                DoStupidSync(newList);
            }
            else
            {
                DoFancySync(newList);
            }

            this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            Refresh();
        }

        private void DoFancySync(IEnumerable<T> newList)
        {
            var someList = new List<T>();

            var keySelector = this.SyncParameters.KeySelector;

            // If an item in source was not in newList, that item would not make it past the GroupJoin.
            // If an item in source is present in newList, that item would be passed to the result selector as oldItem,
            // the matching item would be passed as newItem.
            // If an item was in newList and not in sourceList, that item would be passed to the result selector as newItem,
            // and oldItem would be set to its default.
            var what = newList.GroupJoin(source, keySelector, keySelector, (left, right) => new GroupJoinResult(left, right))
                    .SelectMany(temp => temp.Old.DefaultIfEmpty(), PassOldAndNewValuesIntoSelector).Cast<T>().ToList();

            source = what;
        }

        private object PassOldAndNewValuesIntoSelector(GroupJoinResult arg1, T arg2)
        {            
            if (arg1.Old.Count() == 0)
            {
                return this.SyncParameters.ResultSelector(default(T), arg1.New);                
            }

            return this.SyncParameters.ResultSelector(arg1.Old.First(), arg1.New);
        }

        private class GroupJoinResult
        {
            public T New { get; set; }
            public IEnumerable<T> Old { get; set; }
            public GroupJoinResult(T left, IEnumerable<T> right)
            {
                New = left;
                Old = right;
            }
        }

        private void DoStupidSync(IEnumerable<T> newList)
        {
            source = newList.ToList();
        }

        public void Refresh()
        {
            var sourceWithFiltersApplied = source
                .Where(ItemPassesFilters).ToList();

            if (OrderingFunction != null)
            {
                var orderedRange = OrderingFunction.Invoke(sourceWithFiltersApplied).ToList();

                Items.Clear();

                foreach (var item in orderedRange)
                {
                    Items.Add(item);
                }

                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, orderedRange, 0, 0));
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