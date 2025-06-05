using FunctionalUtilities;
using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Effects;
using Strawhenge.Inventory.ImportExport;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Procedures;

namespace Strawhenge.Inventory
{
    public class Inventory
    {
        readonly InventoryItemFactory _itemFactory;
        readonly InventoryApparelPieceFactory _apparelPieceFactory;
        readonly InventoryItemLocator _itemLocator;
        readonly IItemRepository _itemRepository;
        readonly ProcedureQueue _procedureQueue;
        readonly InventoryStateImporter _stateImporter;
        readonly InventoryStateExporter _stateExporter;

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

            _itemLocator = new InventoryItemLocator(Hands, Holsters, StoredItems);
            _itemRepository = itemRepository;

            _stateImporter = new InventoryStateImporter(this, logger);
            _stateExporter = new InventoryStateExporter(this);
        }

        public Hands Hands { get; }

        public Holsters Holsters { get; }

        public StoredItems StoredItems { get; }

        public ApparelSlots ApparelSlots { get; }

        public InventoryItem CreateItem(Item item)
        {
            return CreateItem(item, new Context());
        }

        public InventoryItem CreateItem(Item item, Context context)
        {
            return _itemFactory.Create(item, context);
        }

        public InventoryItem CreateTemporaryItem(Item item)
        {
            return _itemFactory.CreateTemporary(item);
        }

        public InventoryApparelPiece CreateApparelPiece(ApparelPiece apparelPiece)
        {
            return _apparelPieceFactory.Create(apparelPiece);
        }

        public Maybe<InventoryItem> GetItemOrCreateTemporary(string itemName)
        {
            return _itemLocator
                .Locate(itemName)
                .Combine(() => _itemRepository
                    .FindByName(itemName)
                    .Map(CreateTemporaryItem));
        }

        public void Interrupt()
        {
            _procedureQueue.SkipAllScheduledProcedures();
        }

        public void ImportState(InventoryState state)
        {
            _stateImporter.Import(state);
        }

        public InventoryState ExportState()
        {
            return _stateExporter.Export();
        }
    }
}