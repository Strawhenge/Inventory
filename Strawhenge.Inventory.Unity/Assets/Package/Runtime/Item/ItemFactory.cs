using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Effects;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Items.Holsters;
using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Items.Context;
using Strawhenge.Inventory.Unity.Items.Data;
using Strawhenge.Inventory.Unity.Procedures;
using System.Collections.Generic;
using System.Linq;
using ILogger = Strawhenge.Common.Logging.ILogger;

namespace Strawhenge.Inventory.Unity.Items
{
    public class ItemFactory : IItemFactory
    {
        readonly Hands _hands;
        readonly HandScriptsContainer _handScripts;
        readonly Holsters _holsters;
        readonly StoredItems _storedItems;
        readonly HolsterScriptsContainer _holsterScriptsContainer;
        readonly ProcedureQueue _procedureQueue;
        readonly EffectFactory _effectFactory;
        readonly IItemDropPoint _itemDropPoint;
        readonly IProduceItemAnimationHandler _produceItemAnimationHandler;
        readonly IConsumeItemAnimationHandler _consumeItemAnimationHandler;
        readonly ILogger _logger;

        public ItemFactory(
            Hands hands,
            HandScriptsContainer handScripts,
            Holsters holsters,
            StoredItems storedItems,
            HolsterScriptsContainer holsterScriptsContainer,
            ProcedureQueue procedureQueue,
            EffectFactory effectFactory,
            IItemDropPoint itemDropPoint,
            IProduceItemAnimationHandler produceItemAnimationHandler,
            IConsumeItemAnimationHandler consumeItemAnimationHandler,
            ILogger logger)
        {
            _hands = hands;
            _handScripts = handScripts;
            _holsters = holsters;
            _storedItems = storedItems;
            _holsterScriptsContainer = holsterScriptsContainer;
            _procedureQueue = procedureQueue;
            _effectFactory = effectFactory;
            _itemDropPoint = itemDropPoint;
            _produceItemAnimationHandler = produceItemAnimationHandler;
            _consumeItemAnimationHandler = consumeItemAnimationHandler;
            _logger = logger;
        }

        public Item Create(IItemData data)
        {
            var component = new ItemHelper(data, _itemDropPoint);
            return Create(component, data);
        }

        public Item Create(IItemData data, ItemContext context)
        {
            var component = new ItemHelper(data, _itemDropPoint, context);
            return Create(component, data);
        }

        public Item CreateTransient(IItemData data)
        {
            var component = new ItemHelper(data, _itemDropPoint);
            return Create(component, data, isTransient: true);
        }

        Item Create(ItemHelper component, IItemData data, bool isTransient = false)
        {
            var procedures = new ItemProcedures(component, _handScripts, _produceItemAnimationHandler);

            var item = new Item(
                data.Name,
                _hands,
                procedures,
                _procedureQueue,
                data.Size,
                isTransient);

            if (!isTransient)
                item.SetupHolsters(CreateHolstersForItem(component, item));

            data.ConsumableData.Do(consumableData =>
            {
                var effects = consumableData.Effects.Select(_effectFactory.Create);

                var consumableProcedures = new ConsumableProcedures(
                    consumableData,
                    _consumeItemAnimationHandler);

                item.SetupConsumable(consumableProcedures, effects);
            });

            if (data.IsStorable && !isTransient)
                item.SetupStorable(_storedItems, data.Weight);

            return item;
        }

        HolstersForItem CreateHolstersForItem(ItemHelper itemComponent, Item item)
        {
            var holstersForItem = new List<HolsterForItem>();

            foreach (var holsterComponent in _holsterScriptsContainer.HolsterScripts)
            {
                if (!itemComponent.IsHolsterCompatible(holsterComponent, _logger))
                    continue;

                _holsters
                    .FindByName(holsterComponent.Name)
                    .Do(holster =>
                    {
                        var procedures = new HolsterForItemProcedures(
                            itemComponent,
                            _handScripts,
                            holsterComponent,
                            _produceItemAnimationHandler,
                            _logger);

                        holstersForItem.Add(new HolsterForItem(
                            item,
                            holster,
                            procedures,
                            _procedureQueue));
                    });
            }

            return new HolstersForItem(holstersForItem);
        }
    }
}