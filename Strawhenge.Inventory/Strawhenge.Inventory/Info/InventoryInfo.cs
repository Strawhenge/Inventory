using System.Collections.Generic;
using System.Linq;

namespace Strawhenge.Inventory.Info
{
    public class InventoryInfo
    {
        public InventoryInfo(
            IEnumerable<ItemInfo> items,
            IEnumerable<string> equippedApparel)
        {
            Items = items.ToArray();
            EquippedApparel = equippedApparel.ToArray();
        }

        public IReadOnlyList<ItemInfo> Items { get; }

        public IReadOnlyList<string> EquippedApparel { get; }
    }
}