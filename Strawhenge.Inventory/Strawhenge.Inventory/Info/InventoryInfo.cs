using System;
using System.Collections.Generic;
using System.Linq;

namespace Strawhenge.Inventory.Info
{
    public class InventoryInfo
    {
        public static InventoryInfo Empty { get; } = new InventoryInfo(
            items: Array.Empty<ItemInfo>(),
            equippedApparel: Array.Empty<string>());

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