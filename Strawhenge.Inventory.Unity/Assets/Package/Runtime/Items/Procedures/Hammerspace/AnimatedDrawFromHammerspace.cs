using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Items.HoldItemData;
using System;

namespace Strawhenge.Inventory.Unity.Items.Procedures
{
    class AnimatedDrawFromHammerspace : Procedure
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

            _animationHandler.GrabItem += PlaceItemInHand;
            _animationHandler.DrawEnded += End;

            _animationHandler.DrawItem(_animationTrigger);
        }

        protected override void OnSkip()
        {
            End();
        }

        void End()
        {
            if (_hasEnded) return;
            _hasEnded = true;

            _animationHandler.GrabItem -= PlaceItemInHand;
            _animationHandler.DrawEnded -= End;

            PlaceItemInHand();
            _endProcedure();
        }

        void PlaceItemInHand()
        {
            if (_itemInHand) return;
            _itemInHand = true;

            _hand.SetItem(_item, _holdItemData);
        }
    }
}