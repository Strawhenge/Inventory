using Strawhenge.Inventory.Items;
using System.Collections.Generic;

namespace Strawhenge.Inventory
{
    public class ItemInventory : IItemInventory
    {
        readonly List<IItem> items = new List<IItem>();

        public IEnumerable<IItem> AllItems => items.ToArray();

        public void Add(IItem item)
        {
            if (items.Contains(item))
                return;

            items.Add(item);
            item.ClearFromHandsPreference = ClearFromHandsPreference.PutAway;
            item.Dropped += Remove;
        }

        public void Remove(IItem item)
        {
            if (!items.Contains(item))
                return;

            items.Remove(item);
            item.ClearFromHandsPreference = ClearFromHandsPreference.Drop;
            item.Dropped -= Remove;
        }
    }
}
