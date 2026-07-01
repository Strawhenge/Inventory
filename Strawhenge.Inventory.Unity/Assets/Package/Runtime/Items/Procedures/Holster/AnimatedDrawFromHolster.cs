using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Items.HoldItemData;
using System;

namespace Strawhenge.Inventory.Unity.Items.Procedures
{
    class AnimatedDrawFromHolster : Procedure
    {
        readonly ProduceItemAnimationHandler _animationHandler;
        readonly ItemScriptInstance _itemScriptInstance;
        readonly IHoldItemData _holdItemData;
        readonly HolsterScript _holster;
        readonly HandScript _hand;
        readonly int _animationId;

        Action _endProcedure = () => { };
        bool _itemInHand;
        bool _hasEnded;

        public AnimatedDrawFromHolster(
            ProduceItemAnimationHandler animationHandler,
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
            _animationHandler.DrawEnded += End;

            _animationHandler.DrawItem(_animationId);
        }

        protected override void OnSkip()
        {
            End();
            _animationHandler.Interrupt();
        }

        void End()
        {
            if (_hasEnded) return;
            _hasEnded = true;

            _animationHandler.GrabItem -= PutItemInHand;
            _animationHandler.DrawEnded -= End;

            PutItemInHand();
            _endProcedure();
        }

        void PutItemInHand()
        {
            if (_itemInHand) return;
            _itemInHand = true;

            _holster.UnsetItem();
            _hand.SetItem(_itemScriptInstance, _holdItemData);
        }
    }
}