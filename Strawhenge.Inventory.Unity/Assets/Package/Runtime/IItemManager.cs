using Strawhenge.Inventory.Unity.Data;
using Strawhenge.Inventory.Unity.Monobehaviours;
using System.Collections.Generic;
using FunctionalUtilities;

namespace Strawhenge.Inventory.Unity
{
    public interface IItemManager
    {
        Maybe<IItem> ItemInLeftHand { get; }

        Maybe<IItem> ItemInRightHand { get; }

        IEnumerable<IItem> ItemsInHolsters { get; }

        IEnumerable<IItem> ItemsInInventory { get; }

        IItem AddItemToInventory(IItem item);

        IItem AddItemToInventory(IItemData itemData);

        IItem AddItemToInventory(ItemScript itemScript);

        void RemoveItemFromInventory(IItem item);

        IItem Manage(ItemScript itemScript);

        IItem Manage(IItemData itemData);
    }
}
