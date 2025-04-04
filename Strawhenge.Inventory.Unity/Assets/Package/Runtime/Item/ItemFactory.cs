using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Effects;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Items.Holsters;
using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Items.Data;
using Strawhenge.Inventory.Unity.Consumables;
using Strawhenge.Inventory.Unity.Items.Context;
using Strawhenge.Inventory.Unity.Procedures;
using System.Collections.Generic;
using System.Linq;
using ILogger = Strawhenge.Common.Logging.ILogger;
using ItemSize = Strawhenge.Inventory.Items.ItemSize;

namespace Strawhenge.Inventory.Unity.Items
{
    public class ItemFactory : IItemFactory
    {
        readonly Hands _hands;
        readonly Holsters _holsters;
        readonly StoredItems _storedItems;
        readonly HolsterScriptsContainer _holsterScriptsContainer;
        readonly ProcedureQueue _procedureQueue;
        readonly IProcedureFactory _procedureFactory;
        readonly EffectFactory _effectFactory;
        readonly IItemDropPoint _itemDropPoint;
        readonly ILogger _logger;

        public ItemFactory(
            Hands hands,
            Holsters holsters,
            StoredItems storedItems,
            HolsterScriptsContainer holsterScriptsContainer,
            ProcedureQueue procedureQueue,
            IProcedureFactory procedureFactory,
            EffectFactory effectFactory,
            IItemDropPoint itemDropPoint,
            ILogger logger)
        {
            _hands = hands;
            _holsters = holsters;
            _storedItems = storedItems;
            _holsterScriptsContainer = holsterScriptsContainer;
            _procedureQueue = procedureQueue;
            _procedureFactory = procedureFactory;
            _effectFactory = effectFactory;
            _itemDropPoint = itemDropPoint;
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
            var view = new ItemView(component, _procedureQueue, _procedureFactory);

            var item = new Item(data.Name, _hands, view, data.Size, isTransient);

            if (!isTransient)
                item.SetupHolsters(CreateHolstersForItem(component));

            data.ConsumableData.Do(consumableData =>
            {
                var effects = consumableData.Effects.Select(_effectFactory.Create);
                var consumableView = new ConsumableView(_procedureQueue, _procedureFactory, consumableData);

                item.SetupConsumable(consumableView, effects);
            });

            if (data.IsStorable && !isTransient)
                item.SetupStorable(_storedItems, data.Weight);

            return item;
        }

        IEnumerable<(ItemContainer, IHolsterForItemView)> CreateHolstersForItem(ItemHelper itemComponent)
        {
            var holstersForItem = new List<(ItemContainer, IHolsterForItemView)>();

            foreach (var holsterComponent in _holsterScriptsContainer.HolsterScripts)
            {
                if (!itemComponent.IsHolsterCompatible(holsterComponent, _logger))
                    continue;

                var holster = _holsters.FindByName(holsterComponent.Name);

                holster.Do(x =>
                {
                    var view = new HolsterForItemView(itemComponent, holsterComponent, _procedureQueue,
                        _procedureFactory);
                    holstersForItem.Add((x, view));
                });
            }

            return holstersForItem;
        }
    }
}