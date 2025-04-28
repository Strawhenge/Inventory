using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.Data;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Holster
{
    public class AnimatedPutInHolster : Procedure
    {
        readonly IProduceItemAnimationHandler _animationHandler;
        readonly ItemHelper _itemHelper;
        readonly IHolsterItemData _data;
        readonly HandScript _hand;
        readonly HolsterScript _holster;
        readonly int _animationId;

        Action _endProcedure;
        bool _itemInHolster;
        bool _hasEnded;

        public AnimatedPutInHolster(
            IProduceItemAnimationHandler animationHandler,
            ItemHelper itemHelper,
            IHolsterItemData data,
            HandScript hand,
            HolsterScript holster, 
            int animationId)
        {
            _animationHandler = animationHandler;
            _itemHelper = itemHelper;
            _data = data;
            _hand = hand;
            _holster = holster;
            _animationId = animationId;
        }

        protected override void OnBegin(Action endProcedure)
        {
            _endProcedure = endProcedure;

            _animationHandler.ReleaseItem += PutItemInHolster;
            _animationHandler.PutAwayEnded += End;
            _animationHandler.PutAwayItem(_animationId);
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
            _holster.SetItem(_itemHelper, _data);
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