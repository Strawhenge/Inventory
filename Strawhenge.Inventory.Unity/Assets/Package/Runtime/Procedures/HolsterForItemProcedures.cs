using Strawhenge.Inventory.Items.Holsters;
using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.Data;
using Strawhenge.Inventory.Unity.Procedures.DropItem;
using Strawhenge.Inventory.Unity.Procedures.Holster;

namespace Strawhenge.Inventory.Unity.Procedures
{
    class HolsterForItemProcedures : IHolsterForItemProcedures
    {
        readonly ItemScriptInstance _item;
        readonly IItemData _itemData;
        readonly Context _itemContext;
        readonly IHolsterItemData _holsterItemData;
        readonly HandScriptsContainer _handScripts;
        readonly HolsterScript _holster;
        readonly ProduceItemAnimationHandler _produceItemAnimationHandler;

        public HolsterForItemProcedures(
            ItemScriptInstance item,
            IItemData itemData,
            Context itemContext,
            IHolsterItemData holsterItemHolsterItemData,
            HandScriptsContainer handScripts,
            HolsterScript holster,
            ProduceItemAnimationHandler produceItemAnimationHandler)
        {
            _item = item;
            _itemData = itemData;
            _itemContext = itemContext;
            _holsterItemData = holsterItemHolsterItemData;
            _handScripts = handScripts;
            _holster = holster;
            _produceItemAnimationHandler = produceItemAnimationHandler;
        }

        public Procedure DrawLeftHand() =>
            Draw(_handScripts.Left, _itemData.LeftHandHoldData, _holsterItemData.DrawFromHolsterLeftHandId);

        public Procedure DrawRightHand() =>
            Draw(_handScripts.Right, _itemData.RightHandHoldData, _holsterItemData.DrawFromHolsterRightHandId);

        Procedure Draw(HandScript hand, IHoldItemData data, int id)
        {
            if (id == 0)
                return new SimpleDrawFromHolster(_item, data, _holster, hand);

            return new AnimatedDrawFromHolster(
                _produceItemAnimationHandler, 
                _item, 
                data, 
                _holster, 
                hand, 
                animationTrigger: "" // TODO
                );
        }

        public Procedure PutAwayLeftHand() => PutAway(_handScripts.Left, _holsterItemData.PutInHolsterLeftHandId);

        public Procedure PutAwayRightHand() => PutAway(_handScripts.Right, _holsterItemData.PutInHolsterRightHandId);

        Procedure PutAway(HandScript hand, int id)
        {
            if (id == 0)
                return new SimplePutInHolster(_item, _holsterItemData, hand, _holster);

            return new AnimatedPutInHolster(
                _produceItemAnimationHandler,
                _item,
                _holsterItemData,
                hand, 
                _holster, 
                animationTrigger: "" // TODO
                );
        }

        public Procedure Show() => new ShowInHolster(_item, _holsterItemData, _holster);

        public Procedure Hide() => new HideInHolster(_item, _holster);

        public Procedure Drop() => new SimpleDropFromHolster(_item, _itemData, _itemContext, _holster);
    }
}