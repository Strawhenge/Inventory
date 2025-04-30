using System;
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
        readonly IItemRepository _itemRepository;
        readonly ItemFactory _itemFactory;
        readonly ApparelPieceFactory _apparelPieceFactory;

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
            if (Hands.ItemInRightHand
                    .Where(x => x.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase))
                    .HasSome(out var item) ||
                Hands.ItemInLeftHand
                    .Where(x => x.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase))
                    .HasSome(out item))
                return item;

            foreach (var holster in Holsters)
            {
                if (holster.CurrentItem
                    .Where(x => x.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase))
                    .HasSome(out item))
                    return item;
            }

            foreach (var storedItem in StoredItems.Items)
            {
                if (storedItem.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase))
                    return storedItem;
            }

            return _itemRepository
                .FindByName(itemName)
                .Map(CreateTemporaryItem);
        }
    }
}