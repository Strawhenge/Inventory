using FunctionalUtilities;
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
        readonly InventoryItemFactory _itemFactory;
        readonly InventoryApparelPieceFactory _apparelPieceFactory;
        readonly ItemLocator _itemLocator;
        readonly IItemRepository _itemRepository;
        readonly ProcedureQueue _procedureQueue;

        public Inventory(
            IItemProceduresFactory itemProceduresFactory,
            IApparelViewFactory apparelViewFactory,
            IEffectFactoryLocator effectFactoryLocator,
            IItemRepository itemRepository,
            ILogger logger)
        {
            Hands = new Hands();
            Holsters = new Holsters(logger);
            StoredItems = new StoredItems(logger);
            ApparelSlots = new ApparelSlots(logger);

            _procedureQueue = new ProcedureQueue();
            var effectFactory = new EffectFactory(effectFactoryLocator, logger);

            _itemFactory = new InventoryItemFactory(
                Hands,
                Holsters,
                StoredItems,
                _procedureQueue,
                effectFactory,
                itemProceduresFactory);

            _apparelPieceFactory = new InventoryApparelPieceFactory(
                ApparelSlots,
                effectFactory,
                apparelViewFactory,
                logger);

            _itemLocator = new ItemLocator(Hands, Holsters, StoredItems);
            _itemRepository = itemRepository;
        }

        public Hands Hands { get; }

        public Holsters Holsters { get; }

        public StoredItems StoredItems { get; }

        public ApparelSlots ApparelSlots { get; }

        public InventoryItem CreateItem(Item data) => CreateItem(data, new Context());

        public InventoryItem CreateItem(Item data, Context context)
        {
            return _itemFactory.Create(data, context);
        }

        public InventoryItem CreateTemporaryItem(Item data)
        {
            return _itemFactory.CreateTemporary(data);
        }

        public InventoryApparelPiece CreateApparelPiece(ApparelPiece data)
        {
            return _apparelPieceFactory.Create(data);
        }

        public Maybe<InventoryItem> GetItemOrCreateTemporary(string itemName)
        {
            return _itemLocator
                .Locate(itemName)
                .Combine(() => _itemRepository
                    .FindByName(itemName)
                    .Map(CreateTemporaryItem));
        }

        public void Interrupt() => _procedureQueue.SkipAllScheduledProcedures();
    }
}