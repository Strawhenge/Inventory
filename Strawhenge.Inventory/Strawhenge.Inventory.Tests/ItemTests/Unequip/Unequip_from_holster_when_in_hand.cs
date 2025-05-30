﻿using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.Unequip
{
    public class Unequip_from_holster_when_in_hand : BaseItemTest
    {
        readonly Item _hammer;

        public Unequip_from_holster_when_in_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            AddHolster(RightHipHolster);

            _hammer = CreateItem(Hammer, new[] { RightHipHolster });
            _hammer.Holsters[RightHipHolster].Do(x => x.Equip());
            _hammer.HoldRightHand();
            _hammer.Holsters[RightHipHolster].Do(x => x.Unequip());
        }

        protected override Maybe<Item> ExpectedItemInRightHand => _hammer;

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, RightHipHolster, x => x.Show);
            yield return (Hammer, RightHipHolster, x => x.DrawRightHand);
        }
    }
}