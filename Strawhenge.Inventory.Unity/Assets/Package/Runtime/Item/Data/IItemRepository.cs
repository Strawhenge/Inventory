using FunctionalUtilities;

namespace Strawhenge.Inventory.Unity.Items.Data
{
    public interface IItemRepository
    {
        Maybe<IItemData> FindByName(string name);
    }
}
