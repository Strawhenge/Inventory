﻿using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Data;
using Strawhenge.Inventory.Unity.Items;

namespace Strawhenge.Inventory.Unity
{
    public class Inventory : Strawhenge.Inventory.Inventory, IInventory
    {
        readonly IItemFactory _itemFactory;
        readonly IApparelPieceFactory _apparelPieceFactory;

        public Inventory(
            IStoredItems storedItems,
            IHands hands,
            IHolsters holsters,
            IApparelSlots apparelSlots,
            IItemFactory itemFactory,
            IApparelPieceFactory apparelPieceFactory)
            : base(storedItems, hands, holsters, apparelSlots)
        {
            _itemFactory = itemFactory;
            _apparelPieceFactory = apparelPieceFactory;
        }

        public IItem CreateItem(ItemPickupScript pickup) => CreateItem(pickup.PickupItem());

        public IItem CreateItem(IItemData data)
        {
            return _itemFactory.Create(data);
        }

        public IApparelPiece CreateApparelPiece(IApparelPieceData data)
        {
            return _apparelPieceFactory.Create(data);
        }
    }
}