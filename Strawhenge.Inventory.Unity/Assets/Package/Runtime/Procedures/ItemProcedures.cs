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
        readonly IProduceItemAnimationHandler _produceItemAnimationHandler;

        public ItemProcedures(
            ItemHelper item,
            IItemData itemData,
            HandScriptsContainer handScripts,
            IProduceItemAnimationHandler produceItemAnimationHandler)
        {
            _item = item;
            _itemData = itemData;
            _handScripts = handScripts;
            _produceItemAnimationHandler = produceItemAnimationHandler;
        }

        public Procedure AppearLeftHand() => new SimpleDrawFromHammerspace(_item, _handScripts.Left);

        public Procedure AppearRightHand() => new SimpleDrawFromHammerspace(_item, _handScripts.Right);

        public Procedure DrawLeftHand() => Draw(_handScripts.Left, _itemData.LeftHandHoldData);

        public Procedure DrawRightHand() => Draw(_handScripts.Right, _itemData.RightHandHoldData);

        Procedure Draw(HandScript hand, IHoldItemData holdData)
        {
            if (holdData.DrawFromHammerspaceId == 0)
                return new SimpleDrawFromHammerspace(_item, hand);

            return new AnimatedDrawFromHammerspace(_produceItemAnimationHandler, _item, hand,
                holdData.DrawFromHammerspaceId);
        }

        public Procedure PutAwayLeftHand() => PutAway(_handScripts.Left, _itemData.LeftHandHoldData);

        public Procedure PutAwayRightHand() => PutAway(_handScripts.Right, _itemData.RightHandHoldData);

        Procedure PutAway(HandScript hand, IHoldItemData holdData)
        {
            if (holdData.PutInHammerspaceId == 0)
                return new SimplePutInHammerspace(hand);

            return new AnimatedPutInHammerspace(_produceItemAnimationHandler, hand, holdData.PutInHammerspaceId);
        }

        public Procedure DropLeftHand() => new SimpleDropFromHand(_handScripts.Left);

        public Procedure DropRightHand() => new SimpleDropFromHand(_handScripts.Right);

        public Procedure SpawnAndDrop() => new SimpleSpawnAndDrop(_item);

        public Procedure LeftHandToRightHand() => new SimpleSwapHands(_handScripts.Left, _handScripts.Right);

        public Procedure RightHandToLeftHand() => new SimpleSwapHands(_handScripts.Right, _handScripts.Left);

        public Procedure DisappearLeftHand() => new SimplePutInHammerspace(_handScripts.Left);

        public Procedure DisappearRightHand() => new SimplePutInHammerspace(_handScripts.Right);
    }
}