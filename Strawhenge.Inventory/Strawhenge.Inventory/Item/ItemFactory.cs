using System.Linq;
using Strawhenge.Common;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Effects;
using Strawhenge.Inventory.Items.Holsters;
using Strawhenge.Inventory.Procedures;

namespace Strawhenge.Inventory.Items
{
    class ItemFactory
    {
        readonly Hands _hands;
        readonly Containers.Holsters _holsters;
        readonly StoredItems _storedItems;
        readonly ProcedureQueue _procedureQueue;
        readonly EffectFactory _effectFactory;
        readonly IItemProceduresFactory _proceduresFactory;

        public ItemFactory(
            Hands hands,
            Containers.Holsters holsters,
            StoredItems storedItems,
            ProcedureQueue procedureQueue,
            EffectFactory effectFactory,
            IItemProceduresFactory proceduresFactory)
        {
            _hands = hands;
            _holsters = holsters;
            _storedItems = storedItems;
            _procedureQueue = procedureQueue;
            _effectFactory = effectFactory;
            _proceduresFactory = proceduresFactory;
        }

        public Item Create(ItemData data)
        {
            var procedures = _proceduresFactory.Create(data);

            var item = new Item(
                data.Name,
                _hands,
                procedures.ItemProcedures,
                _procedureQueue,
                data.Size
            );

            item.SetupHolsters(
                new HolstersForItem(data.Holsters
                    .Select(holster =>
                    {
                        if (!_holsters[holster.HolsterName].HasSome(out var container))
                            return null;

                        if (!procedures.HolsterProcedures(holster.HolsterName).HasSome(out var holsterProcedures))
                            return null;

                        return new HolsterForItem(item, container, holsterProcedures, _procedureQueue);
                    })
                    .ExcludeNull()));

            if (data.IsStorable)
                item.SetupStorable(_storedItems, data.Weight);

            if (data.Consumable.HasSome(out var consumableItemData) &&
                procedures.ConsumableProcedures.HasSome(out var consumableProcedures))
            {
                var effects = consumableItemData.Effects
                    .Select(x => _effectFactory.Create(x));

                item.SetupConsumable(consumableProcedures, effects);
            }

            return item;
        }

        public Item CreateTransient(ItemData data)
        {
            var procedures = _proceduresFactory.Create(data);

            var item = new Item(
                data.Name,
                _hands,
                procedures.ItemProcedures,
                _procedureQueue,
                data.Size,
                isTransient: true
            );

            return item;
        }
    }
}