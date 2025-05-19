namespace Strawhenge.Inventory.Items
{
    public class StoreItemResult
    {
        public static StoreItemResult Ok { get; } =
            new StoreItemResult { WasStored = true };

        public static StoreItemResult InsufficientCapacity { get; } =
            new StoreItemResult { HasInsufficientCapacity = true };

        StoreItemResult()
        {
        }

        public bool WasStored { get; private set; }

        public bool HasInsufficientCapacity { get; private set; }
    }
}