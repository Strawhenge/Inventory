using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Containers;

namespace Strawhenge.Inventory
{
    public interface IInventory<TItemSource, TApparelSource>
    {
        IItemContainer LeftHand { get; }

        IItemContainer RightHand { get; }

        IEnumerable<IItemContainer> Holsters { get; }

        IEnumerable<IItem> StoredItems { get; }

        IEnumerable<IApparelSlot> ApparelSlots { get; }

        IItem AddItemToStorage(IItem item);

        IItem AddItemToStorage(TItemSource itemSource);

        void RemoveItemFromStorage(IItem item);

        IItem CreateItem(TItemSource itemSource);

        IApparelPiece CreateApparelPiece(TApparelSource apparelSource);
    }
}