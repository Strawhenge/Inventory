using System.Collections.Generic;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Containers;

namespace Strawhenge.Inventory
{
    public interface IInventory
    {
        IItemContainer LeftHand { get; }

        IItemContainer RightHand { get; }

        IEnumerable<IItemContainer> Holsters { get; }

        IStoredItems StoredItems { get; }

        IEnumerable<IApparelSlot> ApparelSlots { get; }

        IItem AddToStorage(IItem item);

        void RemoveFromStorage(IItem item);
    }
}