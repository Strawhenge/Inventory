﻿using System.Collections.Generic;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.Drop
{
    public class Drop_when_equipped_to_holster_and_in_hand : BaseItemTest
    {
        public Drop_when_equipped_to_holster_and_in_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            AddHolster(RightHipHolster);

            var hammer = CreateItem(Hammer, new[] { RightHipHolster });
            hammer.Holsters[RightHipHolster].Do(x => x.Equip());
            hammer.HoldRightHand();
            hammer.Drop();
        }

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, RightHipHolster, x => x.Show);
            yield return (Hammer, RightHipHolster, x => x.DrawRightHand);

            yield return (Hammer, x => x.DropRightHand);
        }
    }
}