namespace Strawhenge.Inventory.Unity
{
    public class CanAlwaysBeLooted : ILootInventoryChecker
    {
        public static CanAlwaysBeLooted Instance { get; } = new CanAlwaysBeLooted();

        CanAlwaysBeLooted()
        {
        }

        public bool CanBeLooted => true;
    }
}