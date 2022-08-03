using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Hammerspace
{
    public class DisappearItem : Procedure
    {
        private readonly IItemHelper item;
        private readonly IHandComponent leftHand;
        private readonly IHandComponent rightHand;

        public DisappearItem(IItemHelper item, IHandComponent leftHand, IHandComponent rightHand)
        {
            this.item = item;
            this.leftHand = leftHand;
            this.rightHand = rightHand;
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

        private void Disapper()
        {
            item.Despawn();

            if (leftHand.Item.HasSome(out var leftItem) && leftItem == item)
                leftHand.TakeItem();

            if (rightHand.Item.HasSome(out var rightItem) && rightItem == item)
                rightHand.TakeItem();
        }
    }
}
