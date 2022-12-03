using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Data;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Procedures.DropItem;
using Strawhenge.Inventory.Unity.Procedures.Hammerspace;
using Strawhenge.Inventory.Unity.Procedures.Holster;
using Strawhenge.Inventory.Unity.Procedures.SwapHands;

namespace Strawhenge.Inventory.Unity.Procedures
{
    public class ProcedureFactory : IProcedureFactory
    {
        private readonly IHandComponents handComponents;
        private readonly IProduceItemAnimationHandler produceItemAnimationHandler;
        private readonly IItemDropPoint itemDropPoint;
        private readonly ILogger logger;

        public ProcedureFactory(
            IHandComponents handComponents,
            IProduceItemAnimationHandler produceItemAnimationHandler,
            IItemDropPoint itemDropPoint,
            ILogger logger)
        {
            this.handComponents = handComponents;
            this.produceItemAnimationHandler = produceItemAnimationHandler;
            this.itemDropPoint = itemDropPoint;
            this.logger = logger;
        }

        public Procedure DrawLeftHandFromHammerspace(IItemHelper item) => DrawFromHammerspace(item, handComponents.Left, item.Data.LeftHandHoldData);

        public Procedure DrawRightHandFromHammerspace(IItemHelper item) => DrawFromHammerspace(item, handComponents.Right, item.Data.RightHandHoldData);

        public Procedure DropFromLeftHand(IItemHelper item) => new SimpleDropFromHand(handComponents.Left);

        public Procedure DropFromRightHand(IItemHelper item) => new SimpleDropFromHand(handComponents.Right);

        public Procedure PutAwayLeftHandToHammerspace(IItemHelper item) => PutAwayToHammerspace(handComponents.Left, item.Data.LeftHandHoldData);

        public Procedure PutAwayRightHandToHammerspace(IItemHelper item) => PutAwayToHammerspace(handComponents.Right, item.Data.RightHandHoldData);

        public Procedure SwapFromLeftHandToRightHand(IItemHelper item) => new SimpleSwapHands(handComponents.Left, handComponents.Right);

        public Procedure SwapFromRightHandToLeftHand(IItemHelper item) => new SimpleSwapHands(handComponents.Right, handComponents.Left);

        public Procedure SpawnAndDrop(IItemHelper item) => new SimpleSpawnAndDrop(item, itemDropPoint);

        public Procedure Disappear(IItemHelper item) => new DisappearItem(item, handComponents.Left, handComponents.Right);

        public Procedure DrawLeftHandFromHolster(IItemHelper item, IHolsterComponent holster) =>
            DrawFromHolster(handComponents.Left, holster, item.GetHolsterData(holster, logger).DrawFromHolsterLeftHandId);

        public Procedure DrawRightHandFromHolster(IItemHelper item, IHolsterComponent holster) =>
            DrawFromHolster(handComponents.Right, holster, item.GetHolsterData(holster, logger).DrawFromHolsterRightHandId);

        public Procedure PutAwayLeftHandToHolster(IItemHelper item, IHolsterComponent holster) =>
            PutAwayToHolster(handComponents.Left, holster, item.GetHolsterData(holster, logger).PutInHolsterLeftHandId);

        public Procedure PutAwayRightHandToHolster(IItemHelper item, IHolsterComponent holster) =>
            PutAwayToHolster(handComponents.Right, holster, item.GetHolsterData(holster, logger).PutInHolsterRightHandId);

        public Procedure ShowInHolster(IItemHelper item, IHolsterComponent holster) => new ShowInHolster(item, holster);

        public Procedure HideInHolster(IItemHelper item, IHolsterComponent holster) => new HideInHolster(holster);

        private Procedure PutAwayToHammerspace(IHandComponent hand, IHoldItemData holdData)
        {
            if (holdData.PutInHammerspaceId == 0)
                return new SimplePutInHammerspace(hand);

            return new AnimatedPutInHammerspace(produceItemAnimationHandler, hand, holdData.PutInHammerspaceId);
        }

        private Procedure DrawFromHammerspace(IItemHelper item, IHandComponent hand, IHoldItemData holdData)
        {
            if (holdData.DrawFromHammerspaceId == 0)
                return new SimpleDrawFromHammerspace(item, hand);

            return new AnimatedDrawFromHammerspace(produceItemAnimationHandler, item, hand, holdData.DrawFromHammerspaceId);
        }
        private Procedure PutAwayToHolster(IHandComponent hand, IHolsterComponent holster, int id)
        {
            if (id == 0)
                return new SimplePutInHolster(hand, holster);

            return new AnimatedPutInHolster(produceItemAnimationHandler, hand, holster, id);
        }
        private Procedure DrawFromHolster(IHandComponent hand, IHolsterComponent holster, int id)
        {
            if (id == 0)
                return new SimpleDrawFromHolster(holster, hand);

            return new AnimatedDrawFromHolster(produceItemAnimationHandler, holster, hand, id);
        }
    }
}
