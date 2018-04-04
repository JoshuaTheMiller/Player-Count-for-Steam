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

            this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
