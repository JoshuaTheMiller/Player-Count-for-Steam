using System.Collections.Generic;

namespace Trfc.ClientFramework
{
    public static class RangedObservableCollectionExtensions
    {
        public static void ReplaceWithRange<T>(this IRangedCollection<T> collection, IEnumerable<T> range)
        {
            collection.Clear();
            collection.AddRange(range);
        }
    }
}
