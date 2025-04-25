using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Effects;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.Data;
using Strawhenge.Inventory.Unity.Items.Data.ScriptableObjects;

namespace Strawhenge.Inventory.Unity
{
    public class Inventory : Strawhenge.Inventory.Inventory
    {
        public Inventory(
            StoredItems storedItems,
            Hands hands,
            Holsters holsters,
            ApparelSlots apparelSlots,
            ProcedureQueue procedureQueue,
            IItemProceduresFactory itemProceduresFactory,
            IEffectFactoryLocator effectFactoryLocator,
            IApparelViewFactory apparelViewFactory,
            ILogger logger)
            : base(
                storedItems,
                hands,
                holsters,
                apparelSlots,
                procedureQueue,
                itemProceduresFactory,
                apparelViewFactory,
                effectFactoryLocator,
                logger)
        {
        }

        public Item CreateItem(ItemPickupScript pickup)
        {
            var data = pickup.PickupItem();

            return CreateItem(data);
        }

        public Item CreateItem(IItemData data)
        {
            var itemData = (data as ItemScriptableObject).ToItemData();

            var item = base.CreateItem(itemData);

            return item;
        }

        public ApparelPiece CreateApparelPiece(IApparelPieceData data)
        {
            var apparelPieceData = (data as ApparelPieceScriptableObject).ToApparelPieceData();

            var apparelPiece = base.CreateApparelPiece(apparelPieceData);

            return apparelPiece;
        }
    }
}