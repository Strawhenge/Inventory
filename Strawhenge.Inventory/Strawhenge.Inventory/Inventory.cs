using System.Collections.Generic;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Containers;

namespace Strawhenge.Inventory
{
    public class Inventory : IInventory
    {
        readonly IStoredItems _storedItems;
        readonly IHands _hands;
        readonly IHolsters _holsters;
        readonly IApparelSlots _apparelSlots;

        public Inventory(
            IStoredItems storedItems,
            IHands hands,
            IHolsters holsters,
            IApparelSlots apparelSlots)
        {
            _storedItems = storedItems;
            _hands = hands;
            _holsters = holsters;
            _apparelSlots = apparelSlots;
        }

        public IItemContainer LeftHand => _hands.LeftHand;

        public IItemContainer RightHand => _hands.RightHand;

        public IEnumerable<IItemContainer> Holsters => _holsters.GetAll();

        public IEnumerable<IItem> StoredItems => _storedItems.AllItems;

        public IEnumerable<IApparelSlot> ApparelSlots => _apparelSlots.All;

        public IItem AddToStorage(IItem item)
        {
            _storedItems.Add(item);
            return item;
        }

        public void RemoveFromStorage(IItem item)
        {
            _storedItems.Remove(item);
        }
    }
}