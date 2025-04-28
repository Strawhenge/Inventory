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
        readonly ItemHelper _item;
        readonly IItemData _itemData;
        readonly IHolsterItemData _holsterItemData;
        readonly HandScriptsContainer _handScripts;
        readonly HolsterScript _holster;
        readonly IProduceItemAnimationHandler _produceItemAnimationHandler;

        public HolsterForItemProcedures(
            ItemHelper item,
            IItemData itemData,
            IHolsterItemData holsterItemHolsterItemData,
            HandScriptsContainer handScripts,
            HolsterScript holster,
            IProduceItemAnimationHandler produceItemAnimationHandler)
        {
            _item = item;
            _itemData = itemData;
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

            return new AnimatedDrawFromHolster(_produceItemAnimationHandler, _item, data, _holster, hand, id);
        }

        public Procedure PutAwayLeftHand() => PutAway(_handScripts.Left, _holsterItemData.PutInHolsterLeftHandId);

        public Procedure PutAwayRightHand() => PutAway(_handScripts.Right, _holsterItemData.PutInHolsterRightHandId);

        Procedure PutAway(HandScript hand, int id)
        {
            if (id == 0)
                return new SimplePutInHolster(_item, _holsterItemData, hand, _holster);

            return new AnimatedPutInHolster(_produceItemAnimationHandler, _item, _holsterItemData, hand, _holster, id);
        }

        public Procedure Show() => new ShowInHolster(_item, _holsterItemData, _holster);

        public Procedure Hide() => new HideInHolster(_holster);

        public Procedure Drop() => new SimpleDropFromHolster(_item, _itemData, _holster);
    }
}