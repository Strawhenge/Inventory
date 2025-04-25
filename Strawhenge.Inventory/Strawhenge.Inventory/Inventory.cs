using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Containers;

namespace Strawhenge.Inventory
{
    public class Inventory : IInventory
    {
        public Inventory(
            StoredItems storedItems,
            Hands hands,
            Holsters holsters,
            ApparelSlots apparelSlots)
        {
            Hands = hands;
            Holsters = holsters;
            ApparelSlots = apparelSlots;
            StoredItems = storedItems;
        }

        public Hands Hands { get; }

        public Holsters Holsters { get; }

        public StoredItems StoredItems { get; }

        public ApparelSlots ApparelSlots { get; }
    }
}