﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Trfc.ClientFramework
{
    public abstract class BindableObject : INotifyPropertyChanged
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

        protected bool SetField<T>(ref T field, T newValue, [CallerMemberName] string propertyName = "")
        {
            if(field.Equals(newValue))
            {
                return false;
            }

            field = newValue;

            NotifyPropertyChanged(propertyName);

            return true;
        }

        protected bool SetField<T>(ref T field, T newValue, Action<T> callback, [CallerMemberName] string propertyName = "")
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