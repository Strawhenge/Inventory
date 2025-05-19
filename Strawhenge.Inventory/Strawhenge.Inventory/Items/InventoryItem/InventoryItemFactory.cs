using System.Linq;
using Strawhenge.Common;
using Strawhenge.Inventory.Effects;
using Strawhenge.Inventory.Procedures;

namespace Strawhenge.Inventory.Items
{
    class InventoryItemFactory
    {
        readonly Hands _hands;
        readonly Holsters _holsters;
        readonly StoredItems _storedItems;
        readonly ProcedureQueue _procedureQueue;
        readonly EffectFactory _effectFactory;
        readonly IItemProceduresFactory _proceduresFactory;

        public InventoryItemFactory(
            Hands hands,
            Holsters holsters,
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

        public InventoryItem Create(Item data, Context context)
        {
            var procedures = _proceduresFactory.Create(data, context);

            var item = new InventoryItem(
                data,
                context,
                _hands,
                procedures.ItemProcedures,
                _procedureQueue
            );

            item.SetupHolsters(
                new InventoryItemHolsters(data.Holsters
                    .Select(holster =>
                    {
                        if (!_holsters[holster.HolsterName].HasSome(out var container))
                            return null;

                        if (!procedures.HolsterProcedures(holster.HolsterName).HasSome(out var holsterProcedures))
                            return null;

                        return new InventoryItemHolster(item, container, holsterProcedures, _procedureQueue);
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

        public InventoryItem CreateTemporary(Item data)
        {
            var context = new Context();
            var procedures = _proceduresFactory.Create(data, context);

            var item = new InventoryItem(
                data,
                context,
                _hands,
                procedures.ItemProcedures,
                _procedureQueue,
                isTemporary: true
            );

            return item;
        }
    }
}