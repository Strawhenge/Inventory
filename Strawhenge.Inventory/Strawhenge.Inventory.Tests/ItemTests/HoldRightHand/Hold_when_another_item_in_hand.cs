﻿using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.HoldRightHand
{
    public class Hold_when_another_item_in_hand : BaseItemTest
    {
        readonly Item _spear;

        public Hold_when_another_item_in_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            var hammer = CreateItem(Hammer);
            hammer.HoldRightHand();

            _spear = CreateTwoHandedItem(Spear);
            _spear.HoldRightHand();
        }

        protected override Maybe<Item> ExpectedItemInRightHand => _spear;

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, x => x.AppearRightHand);
            yield return (Hammer, x => x.DropRightHand);

            yield return (Spear, x => x.AppearRightHand);
        }
    }
}