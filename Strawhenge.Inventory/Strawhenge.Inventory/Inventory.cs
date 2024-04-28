using System.Collections.Generic;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Containers;

namespace Strawhenge.Inventory
{
    public class Inventory<TItemSource, TApparelSource> : IInventory<TItemSource, TApparelSource>
    {
        readonly IStoredItems _storedItems;
        readonly IHands _hands;
        readonly IHolsters _holsters;
        readonly IApparelSlots _apparelSlots;
        readonly IItemFactory<TItemSource> _itemFactory;
        readonly IApparelPieceFactory<TApparelSource> _apparelPieceFactory;

        public Inventory(
            IStoredItems storedItems,
            IHands hands,
            IHolsters holsters,
            IApparelSlots apparelSlots,
            IItemFactory<TItemSource> itemFactory,
            IApparelPieceFactory<TApparelSource> apparelPieceFactory)
        {
            _storedItems = storedItems;
            _hands = hands;
            _holsters = holsters;
            _apparelSlots = apparelSlots;
            _itemFactory = itemFactory;
            _apparelPieceFactory = apparelPieceFactory;
        }

        public IItemContainer LeftHand => _hands.LeftHand;

        public IItemContainer RightHand => _hands.RightHand;

        public IEnumerable<IItemContainer> Holsters => _holsters.GetAll();

        public IEnumerable<IItem> StoredItems => _storedItems.AllItems;

        public IEnumerable<IApparelSlot> ApparelSlots => _apparelSlots.All;

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

        public IApparelPiece CreateApparelPiece(TApparelSource apparelSource)
        {
            return _apparelPieceFactory.Create(apparelSource);
        }
    }
}