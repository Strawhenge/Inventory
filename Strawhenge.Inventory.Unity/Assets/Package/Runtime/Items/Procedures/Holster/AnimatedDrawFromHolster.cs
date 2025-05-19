using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Items.HoldItemData;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Items.Procedures
{
    public class AnimatedDrawFromHolster : Procedure
    {
        readonly ProduceItemAnimationHandler _animationHandler;
        readonly ItemScriptInstance _itemScriptInstance;
        readonly IHoldItemData _holdItemData;
        readonly HolsterScript _holster;
        readonly HandScript _hand;
        readonly string _animationTrigger;

        Action _endProcedure = () => { };
        bool _itemInHand;
        bool _hasEnded;

        public AnimatedDrawFromHolster(
            ProduceItemAnimationHandler animationHandler,
            ItemScriptInstance itemScriptInstance,
            IHoldItemData holdItemData,
            HolsterScript holster,
            HandScript hand,
            string animationTrigger)
        {
            _animationHandler = animationHandler;
            _itemScriptInstance = itemScriptInstance;
            _holdItemData = holdItemData;
            _holster = holster;
            _hand = hand;
            _animationTrigger = animationTrigger;
        }

        protected override void OnBegin(Action endProcedure)
        {
            _endProcedure = endProcedure;

            _animationHandler.GrabItem += PutItemInHand;
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