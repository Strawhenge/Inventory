using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Consumables;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.Data;

namespace Strawhenge.Inventory.Unity.Procedures
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

        public ItemProcedureDto Create(Item itemData, Context context)
        {
            var unityItemData = itemData
                .Get<IItemData>()
                .Reduce(() => NullItemData.Instance);

            var itemHelper = new ItemScriptInstance(unityItemData.Prefab, context);

            var itemProcedures = new ItemProcedures(
                itemHelper,
                unityItemData,
                context,
                _handScripts,
                _dropPoint,
                _produceItemAnimationHandler);

            var dto = new ItemProcedureDto(itemProcedures);

            foreach (var holsterData in itemData.Holsters)
            {
                var unityHolsterData = holsterData
                    .Get<IHolsterItemData>()
                    .Reduce(() => NullHolsterItemData.Instance);

                _holsterScripts[holsterData.HolsterName]
                    .Do(holsterScript =>
                    {
                        var holsterProcedures = new ItemHolsterProcedures(
                            itemHelper,
                            unityItemData,
                            context,
                            unityHolsterData,
                            _handScripts,
                            holsterScript,
                            _produceItemAnimationHandler);

                        dto.SetHolster(holsterData.HolsterName, holsterProcedures);
                    });
            }

            itemData.Consumable
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