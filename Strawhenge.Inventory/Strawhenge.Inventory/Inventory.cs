using System.Collections.Generic;
using Strawhenge.Inventory.Containers;

namespace Strawhenge.Inventory
{
    public class Inventory<TItemSource> : IInventory<TItemSource>
    {
        readonly IStoredItems _storedItems;
        readonly IHands _hands;
        readonly IHolsters _holsters;
        readonly IItemFactory<TItemSource> _itemFactory;

        public Inventory(
            IStoredItems storedItems,
            IHands hands,
            IHolsters holsters,
            IItemFactory<TItemSource> itemFactory)
        {
            _storedItems = storedItems;
            _hands = hands;
            _holsters = holsters;
            _itemFactory = itemFactory;
        }

        public IItemContainer LeftHand => _hands.LeftHand;

        public IItemContainer RightHand => _hands.RightHand;

        public IEnumerable<IItemContainer> Holsters => _holsters.GetAll();

        public IEnumerable<IItem> StoredItems => _storedItems.AllItems;

        public IItem AddItemToStorage(IItem item)
        {
            _storedItems.Add(item);
            return item;
        }

        public IItem AddItemToStorage(TItemSource itemSource)
        {
            return AddItemToStorage(CreateItem(itemSource));
        }

        public void RemoveItemFromStorage(IItem item)
        {
            _storedItems.Remove(item);
        }

        public IItem CreateItem(TItemSource itemSource)
        {
            return _itemFactory.Create(itemSource);
        }
    }
}