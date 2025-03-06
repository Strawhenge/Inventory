using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Items.Data;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.Context;

namespace Strawhenge.Inventory.Unity
{
    public class Inventory : Strawhenge.Inventory.Inventory, IInventory
    {
        readonly IItemFactory _itemFactory;
        readonly IApparelPieceFactory _apparelPieceFactory;

        public Inventory(
            StoredItems storedItems,
            Hands hands,
            Holsters holsters,
            IApparelSlots apparelSlots,
            IItemFactory itemFactory,
            IApparelPieceFactory apparelPieceFactory)
            : base(storedItems, hands, holsters, apparelSlots)
        {
            _itemFactory = itemFactory;
            _apparelPieceFactory = apparelPieceFactory;
        }

        public Item CreateItem(ItemPickupScript pickup)
        {
            var context = new ItemContext();
            pickup.ContextOut(context);
            return _itemFactory.Create(pickup.PickupItem(), context);
        }

        public Item CreateItem(IItemData data)
        {
            return _itemFactory.Create(data);
        }

        public ApparelPiece CreateApparelPiece(IApparelPieceData data)
        {
            return _apparelPieceFactory.Create(data);
        }
    }
}