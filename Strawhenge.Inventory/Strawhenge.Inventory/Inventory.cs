using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Effects;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Procedures;

namespace Strawhenge.Inventory
{
    public class Inventory
    {
        readonly ItemFactory _itemFactory;
        readonly ApparelPieceFactory _apparelPieceFactory;

        public Inventory(
            IItemProceduresFactory itemProceduresFactory,
            IApparelViewFactory apparelViewFactory,
            IEffectFactoryLocator effectFactoryLocator,
            ILogger logger)
        {
            Hands = new Hands();
            Holsters = new Holsters(logger);
            StoredItems = new StoredItems(logger);
            ApparelSlots = new ApparelSlots(logger);

            var procedureQueue = new ProcedureQueue();
            var effectFactory = new EffectFactory(effectFactoryLocator, logger);

            _itemFactory = new ItemFactory(
                Hands,
                Holsters,
                StoredItems,
                procedureQueue,
                effectFactory,
                itemProceduresFactory);

            _apparelPieceFactory = new ApparelPieceFactory(
                ApparelSlots,
                effectFactory,
                apparelViewFactory,
                logger);
        }

        public Hands Hands { get; }

        public Holsters Holsters { get; }

        public StoredItems StoredItems { get; }

        public ApparelSlots ApparelSlots { get; }

        public Item CreateItem(ItemData data)
        {
            return _itemFactory.Create(data);
        }

        public Item CreateTransientItem(ItemData data)
        {
            return _itemFactory.CreateTransient(data);
        }

        public ApparelPiece CreateApparelPiece(ApparelPieceData data)
        {
            return _apparelPieceFactory.Create(data);
        }
    }
}