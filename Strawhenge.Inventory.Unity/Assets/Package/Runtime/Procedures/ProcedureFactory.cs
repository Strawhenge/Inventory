using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Items.Data;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Consumables;
using Strawhenge.Inventory.Unity.Procedures.ConsumeItem;
using Strawhenge.Inventory.Unity.Procedures.DropItem;
using Strawhenge.Inventory.Unity.Procedures.Hammerspace;
using Strawhenge.Inventory.Unity.Procedures.Holster;
using Strawhenge.Inventory.Unity.Procedures.SwapHands;

namespace Strawhenge.Inventory.Unity.Procedures
{
    public class ProcedureFactory : IProcedureFactory
    {
        readonly HandScriptContainer _handScriptContainer;
        readonly IProduceItemAnimationHandler _produceItemAnimationHandler;
        readonly IConsumeItemAnimationHandler _consumeItemAnimationHandler;
        readonly ILogger _logger;

        public ProcedureFactory(
            HandScriptContainer handScriptContainer,
            IProduceItemAnimationHandler produceItemAnimationHandler,
            IConsumeItemAnimationHandler consumeItemAnimationHandler,
            ILogger logger)
        {
            _handScriptContainer = handScriptContainer;
            _produceItemAnimationHandler = produceItemAnimationHandler;
            _consumeItemAnimationHandler = consumeItemAnimationHandler;
            _logger = logger;
        }

        public Procedure AppearLeftHand(IItemHelper item) =>
            new SimpleDrawFromHammerspace(item, _handScriptContainer.Left);

        public Procedure AppearRightHand(IItemHelper item) =>
            new SimpleDrawFromHammerspace(item, _handScriptContainer.Right);

        public Procedure DrawLeftHandFromHammerspace(IItemHelper item) =>
            DrawFromHammerspace(item, _handScriptContainer.Left, item.Data.LeftHandHoldData);

        public Procedure DrawRightHandFromHammerspace(IItemHelper item) =>
            DrawFromHammerspace(item, _handScriptContainer.Right, item.Data.RightHandHoldData);

        public Procedure DropFromLeftHand(IItemHelper item) => new SimpleDropFromHand(_handScriptContainer.Left);

        public Procedure DropFromRightHand(IItemHelper item) => new SimpleDropFromHand(_handScriptContainer.Right);

        public Procedure PutAwayLeftHandToHammerspace(IItemHelper item) =>
            PutAwayToHammerspace(_handScriptContainer.Left, item.Data.LeftHandHoldData);

        public Procedure PutAwayRightHandToHammerspace(IItemHelper item) =>
            PutAwayToHammerspace(_handScriptContainer.Right, item.Data.RightHandHoldData);

        public Procedure SwapFromLeftHandToRightHand(IItemHelper item) =>
            new SimpleSwapHands(_handScriptContainer.Left, _handScriptContainer.Right);

        public Procedure SwapFromRightHandToLeftHand(IItemHelper item) =>
            new SimpleSwapHands(_handScriptContainer.Right, _handScriptContainer.Left);

        public Procedure SpawnAndDrop(IItemHelper item) => new SimpleSpawnAndDrop(item);

        public Procedure DisappearLeftHand(IItemHelper item) =>
            new SimplePutInHammerspace(_handScriptContainer.Left);

        public Procedure DisappearRightHand(IItemHelper item) =>
            new SimplePutInHammerspace(_handScriptContainer.Right);

        public Procedure DrawLeftHandFromHolster(IItemHelper item, HolsterScript holster) =>
            DrawFromHolster(_handScriptContainer.Left, holster,
                item.GetHolsterData(holster, _logger).DrawFromHolsterLeftHandId);

        public Procedure DrawRightHandFromHolster(IItemHelper item, HolsterScript holster) =>
            DrawFromHolster(_handScriptContainer.Right, holster,
                item.GetHolsterData(holster, _logger).DrawFromHolsterRightHandId);

        public Procedure PutAwayLeftHandToHolster(IItemHelper item, HolsterScript holster) =>
            PutAwayToHolster(_handScriptContainer.Left, holster,
                item.GetHolsterData(holster, _logger).PutInHolsterLeftHandId);

        public Procedure PutAwayRightHandToHolster(IItemHelper item, HolsterScript holster) =>
            PutAwayToHolster(_handScriptContainer.Right, holster,
                item.GetHolsterData(holster, _logger).PutInHolsterRightHandId);

        public Procedure ShowInHolster(IItemHelper item, HolsterScript holster) => new ShowInHolster(item, holster);

        public Procedure HideInHolster(IItemHelper item, HolsterScript holster) => new HideInHolster(holster);

        public Procedure ConsumeLeftHand(IConsumableData data) => Consume(data.AnimationId, true);

        public Procedure ConsumeRightHand(IConsumableData data) => Consume(data.AnimationId, false);

        Procedure PutAwayToHammerspace(HandScript hand, IHoldItemData holdData)
        {
            if (holdData.PutInHammerspaceId == 0)
                return new SimplePutInHammerspace(hand);

            return new AnimatedPutInHammerspace(_produceItemAnimationHandler, hand, holdData.PutInHammerspaceId);
        }

        Procedure DrawFromHammerspace(IItemHelper item, HandScript hand, IHoldItemData holdData)
        {
            if (holdData.DrawFromHammerspaceId == 0)
                return new SimpleDrawFromHammerspace(item, hand);

            return new AnimatedDrawFromHammerspace(_produceItemAnimationHandler, item, hand,
                holdData.DrawFromHammerspaceId);
        }

        Procedure PutAwayToHolster(HandScript hand, HolsterScript holster, int id)
        {
            if (id == 0)
                return new SimplePutInHolster(hand, holster);

            return new AnimatedPutInHolster(_produceItemAnimationHandler, hand, holster, id);
        }

        Procedure DrawFromHolster(HandScript hand, HolsterScript holster, int id)
        {
            if (id == 0)
                return new SimpleDrawFromHolster(holster, hand);

            return new AnimatedDrawFromHolster(_produceItemAnimationHandler, holster, hand, id);
        }

        Procedure Consume(int animationId, bool inverted)
        {
            if (animationId == 0)
                return Procedure.Completed;

            return new AnimatedConsumeItem(_consumeItemAnimationHandler, animationId, inverted);
        }
    }
}