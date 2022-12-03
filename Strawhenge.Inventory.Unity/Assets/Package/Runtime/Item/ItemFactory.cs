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
        private readonly IEnumerable<Collider> colliders;
        private readonly IHands hands;
        private readonly IHolsters holsters;
        private readonly HolsterComponents holsterComponents;
        private readonly ProcedureQueue procedureQueue;
        private readonly IProcedureFactory procedureFactory;
        private readonly ISpawner spawner;
        private readonly ILogger logger;

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
            colliders = gameObject.GetComponentsInChildren<Collider>();

            this.hands = hands;
            this.holsters = holsters;
            this.holsterComponents = holsterComponents;
            this.procedureQueue = procedureQueue;
            this.procedureFactory = procedureFactory;
            this.spawner = spawner;
            this.logger = logger;
        }

        public IItem Create(ItemScript itemScript)
        {
            var component = new ItemHelper(spawner, colliders, itemScript);
            return Create(component, itemScript.Data);
        }

        public IItem Create(IItemData data)
        {
            var component = new ItemHelper(spawner, colliders, data);
            return Create(component, data);
        }

        private IItem Create(ItemHelper component, IItemData data)
        {
            var view = new ItemView(component, procedureQueue, procedureFactory);

            var itemSize = CreateItemSize(data.Size);

            return new Item(data.Name, hands, view, itemSize,
                getHolstersForItem: x => CreateHolstersForItem(x, component));
        }

        private IHolstersForItem CreateHolstersForItem(IItem item, ItemHelper itemComponent)
        {
            List<IHolsterForItem> holstersForItem = new List<IHolsterForItem>();

            foreach (var holsterComponent in holsterComponents.Components)
            {
                if (!itemComponent.IsHolsterCompatible(holsterComponent, logger))
                    continue;

                var holster = holsters.FindByName(holsterComponent.Name);

                holster.Do(x =>
                {
                    var view = new HolsterForItemView(itemComponent, holsterComponent, procedureQueue,
                        procedureFactory);
                    holstersForItem.Add(new HolsterForItem(item, x, view));
                });
            }

            return new HolstersForItem(holstersForItem, logger);
        }

        private Inventory.Items.ItemSize CreateItemSize(Data.ItemSize size)
        {
            switch (size)
            {
                case Data.ItemSize.OneHanded:
                    return Inventory.Items.ItemSize.OneHanded;

                default:
                case Data.ItemSize.TwoHanded:
                    return Inventory.Items.ItemSize.TwoHanded;
            }
        }
    }
}