using System.Collections.Generic;

namespace Trfc.ClientFramework
{
    public interface IRangedCollection<T> : ICollection<T>
    {
        void AddRange(IEnumerable<T> itemsToAdd);

        void RemoveRange(IEnumerable<T> itemsToRemove);        
    }
}
