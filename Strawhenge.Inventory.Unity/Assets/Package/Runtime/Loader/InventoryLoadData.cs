using System;

namespace Strawhenge.Inventory.Unity.Loader
{
    public class InventoryLoadData
    {
        public HolsteredItemLoadDataEntry[] HolsteredItems { get; set; } = Array.Empty<HolsteredItemLoadDataEntry>();

        public string[] EquippedApparel { get; set; } = Array.Empty<string>();
    }
}