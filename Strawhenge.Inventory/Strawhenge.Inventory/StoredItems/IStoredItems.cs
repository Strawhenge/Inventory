using System;
using System.Collections.Generic;

namespace Strawhenge.Inventory
{
    public interface IStoredItems
    {
        event Action<IItem> ItemAdded;

        event Action<IItem> ItemRemoved;

        int TotalItemsWeight { get; }

        int MaxItemsWeight { get; }

        IEnumerable<IItem> Items { get; }
    }
}