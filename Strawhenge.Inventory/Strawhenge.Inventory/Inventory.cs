using System.Collections.Generic;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Containers;

namespace Strawhenge.Inventory
{
    public class Inventory : IInventory
    {
        readonly IHands _hands;
        readonly IHolsters _holsters;
        readonly IApparelSlots _apparelSlots;

        public Inventory(
            IStoredItems storedItems,
            IHands hands,
            IHolsters holsters,
            IApparelSlots apparelSlots)
        {
            _hands = hands;
            _holsters = holsters;
            _apparelSlots = apparelSlots;
            StoredItems = storedItems;
        }

        public ItemContainer LeftHand => _hands.LeftHand;

        public ItemContainer RightHand => _hands.RightHand;

        public IEnumerable<ItemContainer> Holsters => _holsters.GetAll();

        public IStoredItems StoredItems { get; }

        public IEnumerable<IApparelSlot> ApparelSlots => _apparelSlots.All;
    }
}