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

        public bool SetField<T>(ref T field, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (field?.Equals(newValue) ?? false)
            {
                return false;
            }

            field = newValue;

            NotifyPropertyChanged(propertyName);

            return true;
        }

        public bool SetField<T>(ref T field, T newValue, Action<T> callback, [CallerMemberName] string propertyName = "")
        {
            if (SetField(ref field, newValue, propertyName))
            {
                callback.Invoke(newValue);
                return true;
            }

            return false;
        }
    }
}
