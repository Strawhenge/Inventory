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

        public InventoryItem Create(Item item, Context context)
        {
            var procedures = _proceduresFactory.Create(item, context);

            var inventoryItem = new InventoryItem(
                item,
                context,
                _hands,
                procedures.ItemProcedures,
                _procedureQueue
            );

            inventoryItem.SetupHolsters(
                new InventoryItemHolsters(item.Holsters
                    .Select(holster =>
                    {
                        if (!_holsters[holster.HolsterName].HasSome(out var container))
                            return null;

                        if (!procedures.HolsterProcedures(holster.HolsterName).HasSome(out var holsterProcedures))
                            return null;

                        return new InventoryItemHolster(inventoryItem, container, holsterProcedures, _procedureQueue);
                    })
                    .ExcludeNull()));

            if (item.IsStorable)
                inventoryItem.SetupStorable(_storedItems, item.Weight);

            if (item.Consumable.HasSome(out var consumableItemData) &&
                procedures.ConsumableProcedures.HasSome(out var consumableProcedures))
            {
                var effects = consumableItemData.Effects
                    .Select(x => _effectFactory.Create(x));

                inventoryItem.SetupConsumable(consumableProcedures, effects);
            }

            return inventoryItem;
        }

        public InventoryItem CreateTemporary(Item item)
        {
            var context = new Context();
            var procedures = _proceduresFactory.Create(item, context);

            var inventoryItem = new InventoryItem(
                item,
                context,
                _hands,
                procedures.ItemProcedures,
                _procedureQueue,
                isTemporary: true
            );

            return inventoryItem;
        }
    }
}