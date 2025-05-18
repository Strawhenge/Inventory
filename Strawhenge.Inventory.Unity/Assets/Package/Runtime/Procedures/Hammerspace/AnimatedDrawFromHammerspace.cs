using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.Data;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Hammerspace
{
    public class AnimatedDrawFromHammerspace : Procedure
    {
        readonly ProduceItemAnimationHandler _animationHandler;
        readonly ItemScriptInstance _item;
        readonly IHoldItemData _holdItemData;
        readonly HandScript _hand;
        readonly string _animationTrigger;

        Action _endProcedure = () => { };
        bool _itemInHand;
        bool _hasEnded;

        public AnimatedDrawFromHammerspace(
            ProduceItemAnimationHandler animationHandler,
            ItemScriptInstance item,
            IHoldItemData holdItemData,
            HandScript hand,
            string animationTrigger)
        {
            _animationHandler = animationHandler;
            _item = item;
            _holdItemData = holdItemData;
            _hand = hand;
            _animationTrigger = animationTrigger;
        }

        protected override void OnBegin(Action endProcedure)
        {
            _endProcedure = endProcedure;

            _animationHandler.GrabItem += AnimationHandler_GrabItem;
            _animationHandler.DrawEnded += AnimationHandler_DrawEnded;

            _animationHandler.DrawItem(_animationTrigger);
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
            _animationHandler.GrabItem -= AnimationHandler_GrabItem;

            if (!_itemInHand)
            {
                AnimationHandler_GrabItem();
            }

            _endProcedure();
        }

        void AnimationHandler_GrabItem()
        {
            _animationHandler.GrabItem -= AnimationHandler_GrabItem;

            _hand.SetItem(_item, _holdItemData);
            _itemInHand = true;
        }
    }
}