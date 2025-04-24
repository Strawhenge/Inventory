using System.Collections.Generic;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Containers;

namespace Strawhenge.Inventory
{
    public class Inventory : IInventory
    {
        readonly Hands _hands;
        readonly Holsters _holsters;
        readonly ApparelSlots _apparelSlots;

        public Inventory(
            StoredItems storedItems,
            Hands hands,
            Holsters holsters,
            ApparelSlots apparelSlots)
        {
            _hands = hands;
            _holsters = holsters;
            _apparelSlots = apparelSlots;
            StoredItems = storedItems;
        }

        public ItemContainer LeftHand => _hands.LeftHand;

        public ItemContainer RightHand => _hands.RightHand;

        public IEnumerable<ItemContainer> Holsters => _holsters.GetAll();

        public StoredItems StoredItems { get; }

        public IEnumerable<ApparelSlot> ApparelSlots => _apparelSlots.All;
    }
}