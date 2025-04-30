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
        readonly ItemFactory _itemFactory;
        readonly ApparelPieceFactory _apparelPieceFactory;
        readonly ItemLocator _itemLocator;
        readonly IItemRepository _itemRepository;

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

            _itemLocator = new ItemLocator(Hands, Holsters, StoredItems);
            _itemRepository = itemRepository;
        }

        public Hands Hands { get; }

        public Holsters Holsters { get; }

        public StoredItems StoredItems { get; }

        public ApparelSlots ApparelSlots { get; }

        public Item CreateItem(ItemData data) => CreateItem(data, new Context());

        public Item CreateItem(ItemData data, Context context)
        {
            return _itemFactory.Create(data, context);
        }

        public Item CreateTemporaryItem(ItemData data)
        {
            return _itemFactory.CreateTemporary(data);
        }

        public ApparelPiece CreateApparelPiece(ApparelPieceData data)
        {
            return _apparelPieceFactory.Create(data);
        }

        public Maybe<Item> GetItemOrCreateTemporary(string itemName)
        {
            return _itemLocator
                .Locate(itemName)
                .Combine(() => _itemRepository
                    .FindByName(itemName)
                    .Map(CreateTemporaryItem));
        }
    }
}