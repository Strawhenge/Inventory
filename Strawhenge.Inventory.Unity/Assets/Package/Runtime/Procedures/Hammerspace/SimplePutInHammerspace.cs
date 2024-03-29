﻿using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Hammerspace
{
    public class SimplePutInHammerspace : Procedure
    {
        readonly IHandComponent _hand;

        public SimplePutInHammerspace(IHandComponent hand)
        {
            _hand = hand;
        }

        protected override void OnBegin(Action endProcedure)
        {
            RemoveItemFromHand();
            endProcedure();
        }

        protected override void OnSkip() => RemoveItemFromHand();

        void RemoveItemFromHand()
        {
            var item = _hand.TakeItem();
            item.Despawn();
        }
    }
}
