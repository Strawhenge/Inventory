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

            return new InventoryInfo(equippedApparel);
        }
    }

    public class InventoryInfo
    {
        public InventoryInfo(IEnumerable<string> equippedApparel)
        {
            EquippedApparel = equippedApparel.ToArray();
        }

        public IReadOnlyList<object> Items { get; } = Array.Empty<object>();

        public IReadOnlyList<string> EquippedApparel { get; }
    }
}