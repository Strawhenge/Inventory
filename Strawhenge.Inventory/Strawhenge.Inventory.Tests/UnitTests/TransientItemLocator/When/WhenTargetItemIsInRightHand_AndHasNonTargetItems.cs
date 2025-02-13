﻿using System.Collections.Generic;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.UnitTests.TransientItemLocatorTests
{
    public class WhenTargetItemIsInRightHand_AndHasNonTargetItems : WhenTargetItemIsInRightHand
    {
        public WhenTargetItemIsInRightHand_AndHasNonTargetItems(ITestOutputHelper testOutputHelper) : base(
            testOutputHelper)
        {
        }

        protected override IItem ItemInLeftHand => NonTargetItem();

        protected override IEnumerable<(string holsterName, IItem item)> ItemsInHolsters()
        {
            yield return (LeftHipHolster, NonTargetItem());
            yield return (RightHipHolster, NonTargetItem(name: TargetItem.Name));
            yield return (BackHolster, NonTargetItem());
        }

        protected override IEnumerable<IItem> ItemsInStorage()
        {
            yield return NonTargetItem();
            yield return NonTargetItem(name: TargetItem.Name);
            yield return NonTargetItem();
        }
    }
}