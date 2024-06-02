using System;
using System.Collections.Generic;

namespace Strawhenge.Inventory
{
    public interface IStoredItems
    {
        event Action<IItem> ItemAdded;

        event Action<IItem> ItemRemoved;

        IEnumerable<IItem> Items { get; }
    }
}