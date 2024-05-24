using System;
using System.Collections.Generic;

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
            return new InventoryInfo();
        }
    }

    public class InventoryInfo
    {
        public IReadOnlyList<object> Items { get; } = Array.Empty<object>();

        public IReadOnlyList<object> Apparel { get; } = Array.Empty<object>();
    }
}