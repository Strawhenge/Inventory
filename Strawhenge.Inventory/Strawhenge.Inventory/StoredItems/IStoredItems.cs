using System;
using System.Collections.Generic;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory
{
    public interface IStoredItems
    {
        event Action<Item> ItemAdded;

        event Action<Item> ItemRemoved;

        int TotalItemsWeight { get; }

        int MaxItemsWeight { get; }

        IEnumerable<Item> Items { get; }
    }
}