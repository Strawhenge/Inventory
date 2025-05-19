using FunctionalUtilities;

namespace Strawhenge.Inventory.Items
{
    public interface IItemRepository
    {
        Maybe<Item> FindByName(string name);
    }
}