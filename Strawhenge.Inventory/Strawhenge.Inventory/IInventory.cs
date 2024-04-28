using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Containers;

namespace Strawhenge.Inventory
{
    public interface IInventory<TItemSource>
    {
        IItemContainer LeftHand { get; }

        IItemContainer RightHand { get; }

        IEnumerable<IItemContainer> Holsters { get; }

        IEnumerable<IItem> StoredItems { get; }

        IItem AddItemToStorage(IItem item);

        IItem AddItemToStorage(TItemSource itemSource);

        void RemoveItemFromStorage(IItem item);

        IItem CreateItem(TItemSource itemSource);
    }
}