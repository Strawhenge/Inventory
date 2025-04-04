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
        readonly HandScriptsContainer _handScriptsContainer;
        readonly IProduceItemAnimationHandler _produceItemAnimationHandler;
        readonly IConsumeItemAnimationHandler _consumeItemAnimationHandler;
        readonly ILogger _logger;

        public ProcedureFactory(
            HandScriptsContainer handScriptsContainer,
            IProduceItemAnimationHandler produceItemAnimationHandler,
            IConsumeItemAnimationHandler consumeItemAnimationHandler,
            ILogger logger)
        {
            _handScriptsContainer = handScriptsContainer;
            _produceItemAnimationHandler = produceItemAnimationHandler;
            _consumeItemAnimationHandler = consumeItemAnimationHandler;
            _logger = logger;
        }

        public Procedure AppearLeftHand(ItemHelper item) =>
            new SimpleDrawFromHammerspace(item, _handScriptsContainer.Left);

        public Procedure AppearRightHand(ItemHelper item) =>
            new SimpleDrawFromHammerspace(item, _handScriptsContainer.Right);

        public Procedure DrawLeftHandFromHammerspace(ItemHelper item) =>
            DrawFromHammerspace(item, _handScriptsContainer.Left, item.Data.LeftHandHoldData);

        public Procedure DrawRightHandFromHammerspace(ItemHelper item) =>
            DrawFromHammerspace(item, _handScriptsContainer.Right, item.Data.RightHandHoldData);

        public Procedure DropFromLeftHand(ItemHelper item) => new SimpleDropFromHand(_handScriptsContainer.Left);

        public Procedure DropFromRightHand(ItemHelper item) => new SimpleDropFromHand(_handScriptsContainer.Right);

        public Procedure PutAwayLeftHandToHammerspace(ItemHelper item) =>
            PutAwayToHammerspace(_handScriptsContainer.Left, item.Data.LeftHandHoldData);

        public Procedure PutAwayRightHandToHammerspace(ItemHelper item) =>
            PutAwayToHammerspace(_handScriptsContainer.Right, item.Data.RightHandHoldData);

        public Procedure SwapFromLeftHandToRightHand(ItemHelper item) =>
            new SimpleSwapHands(_handScriptsContainer.Left, _handScriptsContainer.Right);

        public Procedure SwapFromRightHandToLeftHand(ItemHelper item) =>
            new SimpleSwapHands(_handScriptsContainer.Right, _handScriptsContainer.Left);

        public Procedure SpawnAndDrop(ItemHelper item) => new SimpleSpawnAndDrop(item);

        public Procedure DisappearLeftHand(ItemHelper item) =>
            new SimplePutInHammerspace(_handScriptsContainer.Left);

        public Procedure DisappearRightHand(ItemHelper item) =>
            new SimplePutInHammerspace(_handScriptsContainer.Right);

        public Procedure DrawLeftHandFromHolster(ItemHelper item, HolsterScript holster) =>
            DrawFromHolster(_handScriptsContainer.Left, holster,
                item.GetHolsterData(holster, _logger).DrawFromHolsterLeftHandId);

        public Procedure DrawRightHandFromHolster(ItemHelper item, HolsterScript holster) =>
            DrawFromHolster(_handScriptsContainer.Right, holster,
                item.GetHolsterData(holster, _logger).DrawFromHolsterRightHandId);

        public Procedure PutAwayLeftHandToHolster(ItemHelper item, HolsterScript holster) =>
            PutAwayToHolster(_handScriptsContainer.Left, holster,
                item.GetHolsterData(holster, _logger).PutInHolsterLeftHandId);

        public Procedure PutAwayRightHandToHolster(ItemHelper item, HolsterScript holster) =>
            PutAwayToHolster(_handScriptsContainer.Right, holster,
                item.GetHolsterData(holster, _logger).PutInHolsterRightHandId);

        public Procedure ShowInHolster(ItemHelper item, HolsterScript holster) => new ShowInHolster(item, holster);

        public Procedure HideInHolster(ItemHelper item, HolsterScript holster) => new HideInHolster(holster);

        public Procedure ConsumeLeftHand(IConsumableData data) => Consume(data.AnimationId, true);

        public Procedure ConsumeRightHand(IConsumableData data) => Consume(data.AnimationId, false);

        Procedure PutAwayToHammerspace(HandScript hand, IHoldItemData holdData)
        {
            if (holdData.PutInHammerspaceId == 0)
                return new SimplePutInHammerspace(hand);

            return new AnimatedPutInHammerspace(_produceItemAnimationHandler, hand, holdData.PutInHammerspaceId);
        }

        Procedure DrawFromHammerspace(ItemHelper item, HandScript hand, IHoldItemData holdData)
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