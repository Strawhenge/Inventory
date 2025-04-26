using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Effects;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Apparel;

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

        public ApparelPiece CreateApparelPiece(IApparelPieceData data)
        {
            var apparelPieceData = (data as ApparelPieceScriptableObject).ToApparelPieceData();

            var apparelPiece = base.CreateApparelPiece(apparelPieceData);

            return apparelPiece;
        }
    }
}