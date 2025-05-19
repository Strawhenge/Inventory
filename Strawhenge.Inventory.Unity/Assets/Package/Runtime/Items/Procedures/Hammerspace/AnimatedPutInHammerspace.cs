using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Items.Procedures
{
    public class AnimatedPutInHammerspace : Procedure
    {
        readonly ProduceItemAnimationHandler _animationHandler;
        readonly ItemScriptInstance _itemScriptInstance;
        readonly HandScript _hand;
        readonly string _animationTrigger;

        Action _endProcedure = () => { };
        bool _itemIsPutAway;
        bool _hasEnded;

        public AnimatedPutInHammerspace(
            ProduceItemAnimationHandler animationHandler,
            ItemScriptInstance itemScriptInstance,
            HandScript hand,
            string animationTrigger)
        {
            _animationHandler = animationHandler;
            _itemScriptInstance = itemScriptInstance;
            _hand = hand;
            _animationTrigger = animationTrigger;
        }

        protected override void OnBegin(Action endProcedure)
        {
            _endProcedure = endProcedure;

            _animationHandler.ReleaseItem += PutAwayItem;
            _animationHandler.PutAwayEnded += End;

            _animationHandler.PutAwayItem(_animationTrigger);
        }

        protected override void OnSkip()
        {
            End();
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