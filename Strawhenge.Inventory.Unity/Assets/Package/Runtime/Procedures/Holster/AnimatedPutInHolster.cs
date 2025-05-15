using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.Data;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Holster
{
    public class AnimatedPutInHolster : Procedure
    {
        readonly ProduceItemAnimationHandler _animationHandler;
        readonly ItemScriptInstance _itemScriptInstance;
        readonly IHolsterItemData _data;
        readonly HandScript _hand;
        readonly HolsterScript _holster;
        readonly string _animationTrigger;

        Action _endProcedure;
        bool _itemInHolster;
        bool _hasEnded;

        public AnimatedPutInHolster(
            ProduceItemAnimationHandler animationHandler,
            ItemScriptInstance itemScriptInstance,
            IHolsterItemData data,
            HandScript hand,
            HolsterScript holster, 
            string animationTrigger)
        {
            _animationHandler = animationHandler;
            _itemScriptInstance = itemScriptInstance;
            _data = data;
            _hand = hand;
            _holster = holster;
            _animationTrigger = animationTrigger;
        }

        protected override void OnBegin(Action endProcedure)
        {
            _endProcedure = endProcedure;

            _animationHandler.ReleaseItem += PutItemInHolster;
            _animationHandler.PutAwayEnded += End;
            _animationHandler.PutAwayItem(_animationTrigger);
        }

        protected override void OnSkip()
        {
            if (_hasEnded) return;

            _animationHandler.Interupt();
            End();
        }

        void PutItemInHolster()
        {
            _itemInHolster = true;
            _animationHandler.ReleaseItem -= PutItemInHolster;

            _hand.UnsetItem();
            _holster.SetItem(_itemScriptInstance, _data);
        }

        void End()
        {
            _hasEnded = true;
            _animationHandler.PutAwayEnded -= End;

            if (!_itemInHolster) 
                PutItemInHolster();

            _endProcedure();
        }
    }
}