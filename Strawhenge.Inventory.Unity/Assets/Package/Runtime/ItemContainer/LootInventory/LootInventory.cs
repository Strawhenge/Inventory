namespace Strawhenge.Inventory.Unity
{
    public class LootInventory
    {
        readonly InventoryItemContainerSource _source;
        readonly ILootInventoryChecker _checker;

        public LootInventory(
            InventoryItemContainerSource source,
            ILootInventoryChecker checker)
        {
            _source = source;
            _checker = checker;
        }

        public bool CanBeLooted(out IItemContainerSource source)
        {
            if (!_checker.CanBeLooted)
            {
                source = null;
                return false;
            }

            source = _source;
            return true;
        }
    }
}