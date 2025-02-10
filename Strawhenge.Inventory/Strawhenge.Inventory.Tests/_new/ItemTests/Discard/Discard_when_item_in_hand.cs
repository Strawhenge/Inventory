﻿using System.Collections.Generic;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests._new.Discard
{
    public class Discard_when_item_in_hand : BaseItemTest
    {
        readonly Item _hammer;

        public Discard_when_item_in_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _hammer = CreateItem(Hammer);
            _hammer.HoldRightHand();
            _hammer.Discard();
        }

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, x => x.DrawRightHand);
            yield return (Hammer, x => x.Disappear);
        }
    }
}