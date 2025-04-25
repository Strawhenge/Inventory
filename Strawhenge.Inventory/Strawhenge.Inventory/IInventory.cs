using System.Collections.Generic;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Containers;

namespace Strawhenge.Inventory
{
    public interface IInventory
    {
        Hands Hands { get; }

        Holsters Holsters { get; }

        StoredItems StoredItems { get; }

        ApparelSlots ApparelSlots { get; }
    }
}