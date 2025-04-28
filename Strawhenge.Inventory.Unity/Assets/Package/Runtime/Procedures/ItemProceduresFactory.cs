using FunctionalUtilities;
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
        readonly IProduceItemAnimationHandler _produceItemAnimationHandler;
        readonly IConsumeItemAnimationHandler _consumeItemAnimationHandler;
        readonly DropPoint _dropPoint;

        public ItemProceduresFactory(
            HandScriptsContainer handScripts,
            HolsterScriptsContainer holsterScripts,
            IProduceItemAnimationHandler produceItemAnimationHandler,
            IConsumeItemAnimationHandler consumeItemAnimationHandler,
            DropPoint dropPoint)
        {
            _handScripts = handScripts;
            _holsterScripts = holsterScripts;
            _produceItemAnimationHandler = produceItemAnimationHandler;
            _consumeItemAnimationHandler = consumeItemAnimationHandler;
            _dropPoint = dropPoint;
        }

        public ItemProcedureDto Create(ItemData itemData)
        {
            var unityItemData = itemData
                .Get<IItemData>()
                .Reduce(() => new NullItemData());

            var itemHelper = new ItemHelper(unityItemData);

            var itemProcedures = new ItemProcedures(
                itemHelper,
                unityItemData,
                _handScripts,
                _dropPoint,
                _produceItemAnimationHandler);

            var dto = new ItemProcedureDto(itemProcedures);

            foreach (var holsterData in itemData.Holsters)
            {
                var unityHolsterData = holsterData
                    .Get<IHolsterItemData>()
                    .Reduce(() => new NullHolsterItemData(holsterData.HolsterName));

                _holsterScripts.HolsterScripts
                    .FirstOrNone(x => x.HolsterName == holsterData.HolsterName)
                    .Do(holsterScript =>
                    {
                        var holsterProcedures = new HolsterForItemProcedures(
                            itemHelper,
                            unityItemData,
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