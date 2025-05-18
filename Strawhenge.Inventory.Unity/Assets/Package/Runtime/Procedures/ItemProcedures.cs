using FunctionalUtilities;
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
        readonly ItemScriptInstance _item;
        readonly IItemData _itemData;
        readonly Context _itemContext;
        readonly HandScriptsContainer _handScripts;
        readonly DropPoint _dropPoint;
        readonly ProduceItemAnimationHandler _produceItemAnimationHandler;

        public ItemProcedures(
            ItemScriptInstance item,
            IItemData itemData,
            Context itemContext,
            HandScriptsContainer handScripts,
            DropPoint dropPoint,
            ProduceItemAnimationHandler produceItemAnimationHandler)
        {
            _item = item;
            _itemData = itemData;
            _itemContext = itemContext;
            _handScripts = handScripts;
            _dropPoint = dropPoint;
            _produceItemAnimationHandler = produceItemAnimationHandler;
        }

        public Procedure AppearLeftHand() =>
            new SimpleDrawFromHammerspace(_item, _itemData.LeftHandHoldData, _handScripts.Left);

        public Procedure AppearRightHand() =>
            new SimpleDrawFromHammerspace(_item, _itemData.RightHandHoldData, _handScripts.Right);

        public Procedure DrawLeftHand() => Draw(
            _handScripts.Left,
            _itemData.LeftHandHoldData,
            _itemData.DrawAnimationSettings.DrawLeftHandTrigger);

        public Procedure DrawRightHand() => Draw(
            _handScripts.Right,
            _itemData.RightHandHoldData,
            _itemData.DrawAnimationSettings.DrawRightHandTrigger);

        Procedure Draw(HandScript hand, IHoldItemData holdData, Maybe<string> animationTrigger)
        {
            return animationTrigger
                .Map<Procedure>(trigger => new AnimatedDrawFromHammerspace(
                    _produceItemAnimationHandler,
                    _item,
                    holdData,
                    hand,
                    trigger
                ))
                .Reduce(() => new SimpleDrawFromHammerspace(_item, holdData, hand));
        }

        public Procedure PutAwayLeftHand() =>
            PutAway(_handScripts.Left, _itemData.DrawAnimationSettings.PutAwayLeftHandTrigger);

        public Procedure PutAwayRightHand() =>
            PutAway(_handScripts.Right, _itemData.DrawAnimationSettings.PutAwayRightHandTrigger);

        Procedure PutAway(HandScript hand, Maybe<string> animationTrigger)
        {
            return animationTrigger
                .Map<Procedure>(trigger => new AnimatedPutInHammerspace(
                    _produceItemAnimationHandler,
                    _item,
                    hand,
                    trigger
                ))
                .Reduce(() => new SimplePutInHammerspace(_item, hand));
        }

        public Procedure DropLeftHand() => new SimpleDropFromHand(_item, _itemData, _itemContext, _handScripts.Left);

        public Procedure DropRightHand() => new SimpleDropFromHand(_item, _itemData, _itemContext, _handScripts.Right);

        public Procedure SpawnAndDrop() => new SimpleSpawnAndDrop(_item, _itemData, _itemContext, _dropPoint);

        public Procedure LeftHandToRightHand() => new SimpleSwapHands(
            _item,
            _itemData.RightHandHoldData,
            _handScripts.Left,
            _handScripts.Right);

        public Procedure RightHandToLeftHand() => new SimpleSwapHands(
            _item,
            _itemData.LeftHandHoldData,
            _handScripts.Right,
            _handScripts.Left);

        public Procedure DisappearLeftHand() => new SimplePutInHammerspace(_item, _handScripts.Left);

        public Procedure DisappearRightHand() => new SimplePutInHammerspace(_item, _handScripts.Right);
    }
}