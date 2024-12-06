using FunctionalUtilities;
using Strawhenge.Inventory.Unity.Items.Data;

namespace Strawhenge.Inventory.Unity.Loader
{
    public interface ILoadInventoryItem
    {
        IItemData ItemData { get; }

        Maybe<string> HolsterName { get; }

        bool IsInStorage { get; }

        LoadInventoryItemInHand InHand { get; }
    }
}