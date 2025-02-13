using Strawhenge.Inventory.Items;
using System.Collections.Generic;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.UnitTests.TransientItemLocatorTests
{
    public class WhenItemCanBeGenerated_AndHasNonTargetItems : BaseTransientItemLocatorTest
    {
        public WhenItemCanBeGenerated_AndHasNonTargetItems(ITestOutputHelper testOutputHelper) : base(
            testOutputHelper)
        {
        }

        protected override bool GetItemByName_ShouldReturnTargetItem => true;

        protected override ClearFromHandsPreference ExpectedClearFromHandsPreference =>
            ClearFromHandsPreference.Disappear;

        protected override IItem GenerateItem() => TargetItem;

        protected override IItem ItemInLeftHand => NonTargetItem();

        protected override IItem ItemInRightHand => NonTargetItem();

        protected override IEnumerable<(string holsterName, IItem item)> ItemsInHolsters()
        {
            yield return (LeftHipHolster, NonTargetItem());
            yield return (RightHipHolster, NonTargetItem());
            yield return (BackHolster, NonTargetItem());
        }

        protected override IEnumerable<IItem> ItemsInStorage()
        {
            yield return NonTargetItem();
            yield return NonTargetItem();
            yield return NonTargetItem();
        }
    }
}