using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using System;

namespace Strawhenge.Inventory.Unity.Items.Procedures
{
    class AnimatedPutInHammerspace : Procedure
    {
        readonly ProduceItemAnimationHandler _animationHandler;
        readonly ItemScriptInstance _itemScriptInstance;
        readonly HandScript _hand;
        readonly int _animationId;

        Action _endProcedure = () => { };
        bool _itemIsPutAway;
        bool _hasEnded;

        public AnimatedPutInHammerspace(
            ProduceItemAnimationHandler animationHandler,
            ItemScriptInstance itemScriptInstance,
            HandScript hand,
            int animationId)
        {
            _animationHandler = animationHandler;
            _itemScriptInstance = itemScriptInstance;
            _hand = hand;
            _animationId = animationId;
        }

        protected override void OnBegin(Action endProcedure)
        {
            _endProcedure = endProcedure;

            _animationHandler.ReleaseItem += PutAwayItem;
            _animationHandler.PutAwayEnded += End;

            _animationHandler.PutAwayItem(_animationId);
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

            _animationHandler.ReleaseItem -= PutAwayItem;
            _animationHandler.PutAwayEnded -= End;

            PutAwayItem();
            _endProcedure();
        }

        void PutAwayItem()
        {
            if (_itemIsPutAway) return;
            _itemIsPutAway = true;

            _hand.UnsetItem();
            _itemScriptInstance.Despawn();
        }
    }
}