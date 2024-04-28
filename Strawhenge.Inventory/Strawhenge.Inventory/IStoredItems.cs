using System.Collections.Generic;

namespace Strawhenge.Inventory
{
    public interface IStoredItems
    {
        IEnumerable<IItem> AllItems { get; }

        void Add(IItem item);
        void Remove(IItem item);
    }
}