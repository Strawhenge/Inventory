using FunctionalUtilities;
using Strawhenge.Common.Logging;
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
        readonly IItemDropPoint _itemDropPoint;
        readonly ILogger _logger;

        public ItemProceduresFactory(
            HandScriptsContainer handScripts,
            HolsterScriptsContainer holsterScripts,
            IProduceItemAnimationHandler produceItemAnimationHandler,
            IConsumeItemAnimationHandler consumeItemAnimationHandler,
            IItemDropPoint itemDropPoint,
            ILogger logger)
        {
            _handScripts = handScripts;
            _holsterScripts = holsterScripts;
            _produceItemAnimationHandler = produceItemAnimationHandler;
            _consumeItemAnimationHandler = consumeItemAnimationHandler;
            _itemDropPoint = itemDropPoint;
            _logger = logger;
        }

        public ItemProcedureDto Create(ItemData itemData)
        {
            var itemHelper = new ItemHelper(
                itemData.Get<IItemData>().Reduce(() => new NullItemData()),
                _itemDropPoint);

            var itemProcedures = new ItemProcedures(
                itemHelper,
                _handScripts,
                _produceItemAnimationHandler);

            var dto = new ItemProcedureDto(itemProcedures);

            foreach (var holsterData in itemData.Holsters)
            {
                _holsterScripts.HolsterScripts
                    .FirstOrNone(x => x.Name == holsterData.HolsterName)
                    .Do(holsterScript =>
                    {
                        var holsterProcedures = new HolsterForItemProcedures(
                            itemHelper,
                            _handScripts,
                            holsterScript,
                            _produceItemAnimationHandler,
                            _logger);

                        dto.SetHolster(holsterData.HolsterName, holsterProcedures);
                    });
            }

            itemData.Consumable
                .Do(_ =>
                {
                    dto.SetConsumable(new ConsumableProcedures(
                        itemData.Get<IConsumableData>().Reduce(() => new SerializedConsumableData()),
                        _consumeItemAnimationHandler
                    ));
                });

            return dto;
        }
    }
}