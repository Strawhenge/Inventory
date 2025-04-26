using FunctionalUtilities;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory.Unity.Items.Data
{
    public interface IItemRepository
    {
        Maybe<ItemData> FindByName(string name);
    }
}
