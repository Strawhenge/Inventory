namespace Strawhenge.Inventory.Unity
{
    public interface ILootInventoryChecker
    {
        bool CanBeLooted { get; }
    }
}