using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Effects;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.Context;
using Strawhenge.Inventory.Unity.Items.Data;

namespace Strawhenge.Inventory.Unity
{
    public class Inventory : Strawhenge.Inventory.Inventory
    {
        readonly ItemFactory _itemFactory;

        public Inventory(
            StoredItems storedItems,
            Hands hands,
            Holsters holsters,
            ApparelSlots apparelSlots,
            ItemFactory itemFactory,
            IEffectFactoryLocator effectFactoryLocator,
            IApparelViewFactory apparelViewFactory,
            ILogger logger)
            : base(
                storedItems,
                hands,
                holsters,
                apparelSlots,
                effectFactoryLocator,
                apparelViewFactory,
                logger)
        {
            _itemFactory = itemFactory;
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
            var apparelPieceData = (data as ApparelPieceScriptableObject).ToApparelPieceData();

            var apparelPiece = base.CreateApparelPiece(apparelPieceData);

            return apparelPiece;
        }
    }
}