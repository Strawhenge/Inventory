using FunctionalUtilities;
using Strawhenge.Inventory.Unity.Data;

namespace Strawhenge.Inventory.Unity.NewLoader
{
    public interface ILoadInventoryItem
    {
        IItemData ItemData { get; }

        Maybe<string> HolsterName { get; }

        bool IsInStorage { get; }

        LoadInventoryItemInHand InHand { get; }
    }
}