using FunctionalUtilities;

namespace Strawhenge.Inventory.Items
{
    public interface IItemRepository
    {
        Maybe<ItemData> FindByName(string name);
    }
}