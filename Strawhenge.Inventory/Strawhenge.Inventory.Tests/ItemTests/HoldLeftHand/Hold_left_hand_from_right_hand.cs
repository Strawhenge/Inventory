﻿using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.HoldLeftHand
{
    public class Hold_left_hand_from_right_hand : BaseItemTest
    {
        readonly Item _hammer;

        public Hold_left_hand_from_right_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _hammer = CreateItem(Hammer);
            _hammer.HoldRightHand();
            _hammer.HoldLeftHand();
        }

        protected override Maybe<Item> ExpectedItemInLeftHand => _hammer;

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, x => x.AppearRightHand);
            yield return (Hammer, x => x.RightHandToLeftHand);
        }
    }
}