using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.Data;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Holster
{
    public class AnimatedDrawFromHolster : Procedure
    {
        readonly IProduceItemAnimationHandler _animationHandler;
        readonly ItemScriptInstance _itemScriptInstance;
        readonly IHoldItemData _holdItemData;
        readonly HolsterScript _holster;
        readonly HandScript _hand;
        readonly int _animationId;

        Action _endProcedure = () => { };
        bool _itemInHand;
        bool _hasEnded;

        public AnimatedDrawFromHolster(
            IProduceItemAnimationHandler animationHandler,
            ItemScriptInstance itemScriptInstance,
            IHoldItemData holdItemData,
            HolsterScript holster,
            HandScript hand,
            int animationId)
        {
            _animationHandler = animationHandler;
            _itemScriptInstance = itemScriptInstance;
            _holdItemData = holdItemData;
            _holster = holster;
            _hand = hand;
            _animationId = animationId;
        }

        protected override void OnBegin(Action endProcedure)
        {
            _endProcedure = endProcedure;

            _animationHandler.GrabItem += PutItemInHand;
            _animationHandler.DrawEnded += AnimationHandler_DrawEnded;

            _animationHandler.DrawItem(_animationId);
        }

        protected override void OnSkip()
        {
            if (_hasEnded) return;

            _animationHandler.Interupt();
            AnimationHandler_DrawEnded();
        }

        void AnimationHandler_DrawEnded()
        {
            _hasEnded = true;
            _animationHandler.DrawEnded -= AnimationHandler_DrawEnded;

            if (!_itemInHand) PutItemInHand();

            _endProcedure();
        }

        void PutItemInHand()
        {
            _animationHandler.GrabItem -= PutItemInHand;

            _holster.UnsetItem();
            _hand.SetItem(_itemScriptInstance, _holdItemData);

            _itemInHand = true;
        }
    }
}