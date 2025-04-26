using FunctionalUtilities;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory.Unity.Loader
{
    public interface ILoadInventoryItem
    {
        ItemData ItemData { get; }

        Maybe<string> HolsterName { get; }

        bool IsInStorage { get; }

        LoadInventoryItemInHand InHand { get; }
    }
}