using System.Collections.Generic;

namespace Strawhenge.Inventory
{
    public interface IItemInventory
    {
        IEnumerable<IItem> AllItems { get; }

        void Add(IItem item);
        void Remove(IItem item);
    }
}