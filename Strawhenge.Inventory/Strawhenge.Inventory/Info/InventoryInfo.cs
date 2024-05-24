using System;
using System.Collections.Generic;
using System.Linq;

namespace Strawhenge.Inventory.Info
{
    public class InventoryInfoGenerator
    {
        readonly IInventory _inventory;

        public InventoryInfoGenerator(IInventory inventory)
        {
            _inventory = inventory;
        }

        public InventoryInfo GenerateCurrentInfo()
        {
            var equippedApparel = _inventory.ApparelSlots
                .Select(x => x.CurrentPiece.HasSome(out var apparel) ? apparel.Name : null)
                .Where(x => x != null);

            var storedItems = _inventory.StoredItems.Select(x => new ItemInfo
            {
                ItemName = x.Name,
                IsInStorage = true
            });

            var holsteredItems = _inventory.Holsters
                .Select(x => x.CurrentItem.HasSome(out var item)
                    ? new ItemInfo()
                    {
                        ItemName = item.Name,
                        HolsterName = x.Name
                    }
                    : null)
                .Where(x => x != null);

            return new InventoryInfo(storedItems.Concat(holsteredItems), equippedApparel);
        }
    }

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

    public class ItemInfo
    {
        public string ItemName { get; internal set; }

        public string HolsterName { get; internal set; } = string.Empty;

        public bool IsInStorage { get; internal set; }
    }
}