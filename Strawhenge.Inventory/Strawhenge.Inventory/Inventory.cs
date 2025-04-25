using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Containers;

namespace Strawhenge.Inventory
{
    public class Inventory : IInventory
    {
        readonly Hands _hands;

        public Inventory(
            StoredItems storedItems,
            Hands hands,
            Holsters holsters,
            ApparelSlots apparelSlots)
        {
            _hands = hands;
            Holsters = holsters;
            ApparelSlots = apparelSlots;
            StoredItems = storedItems;
        }

        public ItemContainer LeftHand => _hands.LeftHand;

        public ItemContainer RightHand => _hands.RightHand;

        public Holsters Holsters { get; }

        public StoredItems StoredItems { get; }

        public ApparelSlots ApparelSlots { get; }
    }
}