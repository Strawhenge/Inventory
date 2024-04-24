using System;

namespace Strawhenge.Inventory.Unity.Loader
{
    public class LoadInventoryData
    {
        public HolsteredItemLoadInventoryData[] HolsteredItems { get; set; } = Array.Empty<HolsteredItemLoadInventoryData>();

        public string[] EquippedApparel { get; set; } = Array.Empty<string>();
    }
}