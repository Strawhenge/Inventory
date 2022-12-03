using FunctionalUtilities;

namespace Strawhenge.Inventory.Unity.Data
{
    public interface IItemRepository
    {
        Maybe<IItemData> FindByName(string name);
    }
}
