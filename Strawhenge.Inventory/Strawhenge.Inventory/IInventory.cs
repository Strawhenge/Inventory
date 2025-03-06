using System.Collections.Generic;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Containers;

namespace Strawhenge.Inventory
{
    public interface IInventory
    {
        ItemContainer LeftHand { get; }

        ItemContainer RightHand { get; }

        IEnumerable<ItemContainer> Holsters { get; }

        StoredItems StoredItems { get; }

        IEnumerable<ApparelSlot> ApparelSlots { get; }
    }
}