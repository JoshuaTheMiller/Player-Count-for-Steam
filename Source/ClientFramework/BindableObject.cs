using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Trfc.ClientFramework
{
    public class BindableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }                    

        public bool SetField<T>(ref T field, T newValue, Action<T> callback, [CallerMemberName] string propertyName = "")
        {
            return SetField(ref field, newValue, DefaultCompareOperation, callback, propertyName);
        }

        public bool SetField<T>(ref T field, T newValue, [CallerMemberName] string propertyName = "")
        {
            return SetField(ref field, newValue, DefaultCompareOperation, DefaultCallback, propertyName);
        }

        public bool SetField<T>(ref T field, T newValue, Func<T, T, bool> comparisonOperator, [CallerMemberName] string propertyName = "")
        {
            return SetField(ref field, newValue, comparisonOperator, DefaultCallback, propertyName);
        }

        public bool SetField<T>(ref T field, T newValue, Func<T, T, bool> comparisonOperator, Action<T> callback, [CallerMemberName] string propertyName = "")
        {
            if (comparisonOperator(field, newValue))
            {
                return false;
            }

            field = newValue;

            NotifyPropertyChanged(propertyName);

            callback.Invoke(newValue);

            return true;
        }

        private bool DefaultCompareOperation<T>(T field, T newValue)
        {
            if(field == null && newValue == null)
            {
                return true;
            }

            return field?.Equals(newValue) ?? false;
        }

        private void DefaultCallback<T>(T obj)
        {
        }
    }
}
