using FunctionalUtilities;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Items.HolsterForItem;
using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Data;
using Strawhenge.Inventory.Unity.Monobehaviours;
using Strawhenge.Inventory.Unity.Procedures;
using System.Collections.Generic;
using UnityEngine;
using ILogger = Strawhenge.Common.Logging.ILogger;

namespace Strawhenge.Inventory.Unity.Items
{
    public class ItemFactory : IItemFactory
    {
        readonly IReadOnlyList<Collider> _colliders;
        readonly IHands _hands;
        readonly IHolsters _holsters;
        readonly HolsterComponents _holsterComponents;
        readonly ProcedureQueue _procedureQueue;
        readonly IProcedureFactory _procedureFactory;
        readonly ISpawner _spawner;
        readonly ILogger _logger;

        public ItemFactory(
            GameObject gameObject,
            IHands hands,
            IHolsters holsters,
            HolsterComponents holsterComponents,
            ProcedureQueue procedureQueue,
            IProcedureFactory procedureFactory,
            ISpawner spawner,
            ILogger logger)
        {
            _colliders = gameObject.GetComponentsInChildren<Collider>();

            _hands = hands;
            _holsters = holsters;
            _holsterComponents = holsterComponents;
            _procedureQueue = procedureQueue;
            _procedureFactory = procedureFactory;
            _spawner = spawner;
            _logger = logger;
        }

        public IItem Create(ItemScript itemScript)
        {
            var component = new ItemHelper(_spawner, _colliders, itemScript);
            return Create(component, itemScript.Data);
        }

        public IItem Create(IItemData data)
        {
            var component = new ItemHelper(_spawner, _colliders, data);
            return Create(component, data);
        }

        IItem Create(ItemHelper component, IItemData data)
        {
            var view = new ItemView(component, _procedureQueue, _procedureFactory);

            var itemSize = CreateItemSize(data.Size);

            return new Item(data.Name, _hands, view, itemSize,
                getHolstersForItem: x => CreateHolstersForItem(x, component));
        }

        IHolstersForItem CreateHolstersForItem(IItem item, ItemHelper itemComponent)
        {
            List<IHolsterForItem> holstersForItem = new List<IHolsterForItem>();

            foreach (var holsterComponent in _holsterComponents.Components)
            {
                if (!itemComponent.IsHolsterCompatible(holsterComponent, _logger))
                    continue;

                var holster = _holsters.FindByName(holsterComponent.Name);

                holster.Do(x =>
                {
                    var view = new HolsterForItemView(itemComponent, holsterComponent, _procedureQueue,
                        _procedureFactory);
                    holstersForItem.Add(new HolsterForItem(item, x, view));
                });
            }

            return new HolstersForItem(holstersForItem, _logger);
        }

        Strawhenge.Inventory.Items.ItemSize CreateItemSize(Data.ItemSize size)
        {
            switch (size)
            {
                case Data.ItemSize.OneHanded:
                    return Strawhenge.Inventory.Items.ItemSize.OneHanded;

                default:
                case Data.ItemSize.TwoHanded:
                    return Strawhenge.Inventory.Items.ItemSize.TwoHanded;
            }
        }
    }
}