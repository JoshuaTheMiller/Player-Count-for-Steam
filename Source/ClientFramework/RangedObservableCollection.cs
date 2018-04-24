using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Trfc.ClientFramework
{
    public class RangedObservableCollection<T> : ObservableCollection<T>, IRangedCollection<T>
    {
        public RangedObservableCollection() { }

        public RangedObservableCollection(IEnumerable<T> items) :base(items) { }

        public void AddRange(IEnumerable<T> range)
        {
            foreach (var item in range)
            {
                Items.Add(item);
            }
            
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, range));            
        }

        public void RemoveRange(IEnumerable<T> itemsToRemove)
        {
            foreach (var item in itemsToRemove)
            {
                Items.Remove(item);
            }
                     
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, itemsToRemove));            
        }
    }
}
