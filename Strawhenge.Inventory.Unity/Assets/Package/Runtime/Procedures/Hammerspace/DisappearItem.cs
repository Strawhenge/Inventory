using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Hammerspace
{
    public class DisappearItem : Procedure
    {
        readonly IItemHelper _item;
        readonly IHandComponent _leftHand;
        readonly IHandComponent _rightHand;

        public DisappearItem(IItemHelper item, IHandComponent leftHand, IHandComponent rightHand)
        {
            _item = item;
            _leftHand = leftHand;
            _rightHand = rightHand;
        }

        protected override void OnBegin(Action endProcedure)
        {
            Disapper();
            endProcedure();
        }

        protected override void OnSkip()
        {
            Disapper();
        }

        void Disapper()
        {
            if (_leftHand.Item.HasSome(out var leftItem) && leftItem == _item)
                _leftHand.TakeItem();

            if (_rightHand.Item.HasSome(out var rightItem) && rightItem == _item)
                _rightHand.TakeItem();
            
            _item.Despawn();
        }
    }
}
