namespace Strawhenge.Inventory.Unity.Loader
{
    public class HolsteredItemLoadDataEntry
    {
        public HolsteredItemLoadDataEntry(string itemName, string holsterName)
        {
            ItemName = itemName;
            HolsterName = holsterName;
        }

        public string ItemName { get; }

        public string HolsterName { get; }
    }
}