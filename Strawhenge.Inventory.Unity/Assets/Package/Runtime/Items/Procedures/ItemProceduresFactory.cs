using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Items.ConsumableData;
using Strawhenge.Inventory.Unity.Items.HolsterItemData;
using Strawhenge.Inventory.Unity.Items.ItemData;

namespace Strawhenge.Inventory.Unity.Items.Procedures
{
    public class ItemProceduresFactory : IItemProceduresFactory
    {
        readonly HandScriptsContainer _handScripts;
        readonly HolsterScriptsContainer _holsterScripts;
        readonly ProduceItemAnimationHandler _produceItemAnimationHandler;
        readonly ConsumeItemAnimationHandler _consumeItemAnimationHandler;
        readonly DropPoint _dropPoint;

        public ItemProceduresFactory(
            HandScriptsContainer handScripts,
            HolsterScriptsContainer holsterScripts,
            ProduceItemAnimationHandler produceItemAnimationHandler,
            ConsumeItemAnimationHandler consumeItemAnimationHandler,
            DropPoint dropPoint)
        {
            _handScripts = handScripts;
            _holsterScripts = holsterScripts;
            _produceItemAnimationHandler = produceItemAnimationHandler;
            _consumeItemAnimationHandler = consumeItemAnimationHandler;
            _dropPoint = dropPoint;
        }

        public ItemProcedureDto Create(Item item, Context context)
        {
            var itemData = item
                .Get<IItemData>()
                .Reduce(() => NullItemData.Instance);

            var itemScriptInstance = new ItemScriptInstance(itemData.Prefab, context);

            var itemProcedures = new ItemProcedures(
                itemScriptInstance,
                itemData,
                context,
                _handScripts,
                _dropPoint,
                _produceItemAnimationHandler);

            var dto = new ItemProcedureDto(itemProcedures);

            foreach (var itemHolster in item.Holsters)
            {
                var holsterData = itemHolster
                    .Get<IHolsterItemData>()
                    .Reduce(() => NullHolsterItemData.Instance);

                _holsterScripts[itemHolster.HolsterName]
                    .Do(holsterScript =>
                    {
                        var holsterProcedures = new ItemHolsterProcedures(
                            itemScriptInstance,
                            itemData,
                            context,
                            holsterData,
                            _handScripts,
                            holsterScript,
                            _produceItemAnimationHandler);

                        dto.SetHolster(itemHolster.HolsterName, holsterProcedures);
                    });
            }

            item.Consumable
                .Do(consumableData =>
                {
                    dto.SetConsumable(new ConsumableProcedures(
                        consumableData.Get<IConsumableData>().Reduce(() => new SerializedConsumableData()),
                        _consumeItemAnimationHandler
                    ));
                });

            return dto;
        }
    }
}