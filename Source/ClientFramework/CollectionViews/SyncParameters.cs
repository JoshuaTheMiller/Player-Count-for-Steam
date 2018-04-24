using System;

namespace Trfc.ClientFramework.CollectionViews
{    
    public sealed class SyncParameters<TItem>
    {                        
        internal bool HasDefaultKeySelector { get; }
        public Func<TItem, object> KeySelector { get; }
        public Func<TItem, TItem, TItem> ResultSelector { get; }

        internal SyncParameters(Func<TItem, object> keySelector, Func<TItem, TItem, TItem> resultCreator, bool hasDefaultKeySelector)
        {
            KeySelector = keySelector;
            ResultSelector = resultCreator;
            HasDefaultKeySelector = hasDefaultKeySelector;
        }       
    }

    public static class SyncParameters
    {        
        public static SyncParameters<T> Create<T>(Func<T, object> keySelector, Func<T, T, T> resultCreator)
        {
            return new SyncParameters<T>(keySelector, resultCreator, false);
        }        

        public static SyncParameters<T> WithDefaultResultSelector<T>(Func<T, object> keySelector)
        {
            return Create<T>(keySelector, TakeLeft);
        }

        internal static SyncParameters<T> WithDefaultKeySelector<T>()
        {
            return new SyncParameters<T>(DefaultKeySelector, TakeLeft, true);
        }

        private static object DefaultKeySelector<T>(T obj)
        {
            return obj;
        }

        private static T TakeLeft<T>(T old, T newValue)
        {
            if(old == null)
            {
                return newValue;
            }

            return old;
        }
    }
}
