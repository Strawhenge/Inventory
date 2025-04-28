using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.Data;
using Strawhenge.Inventory.Unity.Procedures.DropItem;
using Strawhenge.Inventory.Unity.Procedures.Hammerspace;
using Strawhenge.Inventory.Unity.Procedures.SwapHands;

namespace Strawhenge.Inventory.Unity.Procedures
{
    class ItemProcedures : IItemProcedures
    {
        readonly ItemHelper _item;
        readonly IItemData _itemData;
        readonly HandScriptsContainer _handScripts;
        readonly DropPoint _dropPoint;
        readonly IProduceItemAnimationHandler _produceItemAnimationHandler;

        public ItemProcedures(
            ItemHelper item,
            IItemData itemData,
            HandScriptsContainer handScripts,
            DropPoint dropPoint,
            IProduceItemAnimationHandler produceItemAnimationHandler)
        {
            _item = item;
            _itemData = itemData;
            _handScripts = handScripts;
            _dropPoint = dropPoint;
            _produceItemAnimationHandler = produceItemAnimationHandler;
        }

        public Procedure AppearLeftHand() =>
            new SimpleDrawFromHammerspace(_item, _itemData.LeftHandHoldData, _handScripts.Left);

        public Procedure AppearRightHand() =>
            new SimpleDrawFromHammerspace(_item, _itemData.RightHandHoldData, _handScripts.Right);

        public Procedure DrawLeftHand() => Draw(_handScripts.Left, _itemData.LeftHandHoldData);

        public Procedure DrawRightHand() => Draw(_handScripts.Right, _itemData.RightHandHoldData);

        Procedure Draw(HandScript hand, IHoldItemData holdData)
        {
            if (holdData.DrawFromHammerspaceId == 0)
                return new SimpleDrawFromHammerspace(_item, holdData, hand);

            return new AnimatedDrawFromHammerspace(
                _produceItemAnimationHandler,
                _item,
                holdData,
                hand,
                holdData.DrawFromHammerspaceId);
        }

        public Procedure PutAwayLeftHand() => PutAway(_handScripts.Left, _itemData.LeftHandHoldData);

        public Procedure PutAwayRightHand() => PutAway(_handScripts.Right, _itemData.RightHandHoldData);

        Procedure PutAway(HandScript hand, IHoldItemData holdData)
        {
            if (holdData.PutInHammerspaceId == 0)
                return new SimplePutInHammerspace(_item, hand);

            return new AnimatedPutInHammerspace(_produceItemAnimationHandler, _item, hand, holdData.PutInHammerspaceId);
        }

        public Procedure DropLeftHand() => new SimpleDropFromHand(_item, _itemData, _handScripts.Left);

        public Procedure DropRightHand() => new SimpleDropFromHand(_item, _itemData, _handScripts.Right);

        public Procedure SpawnAndDrop() => new SimpleSpawnAndDrop(_item, _itemData, _dropPoint);

        public Procedure LeftHandToRightHand() => new SimpleSwapHands(
            _item,
            _itemData.LeftHandHoldData,
            _handScripts.Left,
            _handScripts.Right);

        public Procedure RightHandToLeftHand() => new SimpleSwapHands(
            _item,
            _itemData.RightHandHoldData,
            _handScripts.Right,
            _handScripts.Left);

        public Procedure DisappearLeftHand() => new SimplePutInHammerspace(_item, _handScripts.Left);

        public Procedure DisappearRightHand() => new SimplePutInHammerspace(_item, _handScripts.Right);
    }
}