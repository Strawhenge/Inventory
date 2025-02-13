using Strawhenge.Inventory.Items;
using System.Collections.Generic;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.UnitTests.TransientItemLocatorTests
{
    public class WhenItemCanBeGenerated_AndHasNonTargetItems : BaseTransientItemLocatorTest
    {
        protected WhenItemCanBeGenerated_AndHasNonTargetItems(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override bool GetItemByName_ShouldReturnTargetItem => true;

        protected override ClearFromHandsPreference ExpectedClearFromHandsPreference => ClearFromHandsPreference.Disappear;

        protected override IItem GenerateItem() => TargetItem;

        protected override IItem ItemInLeftHand => NonTargetItem();

        protected override IItem ItemInRightHand => NonTargetItem();

        protected override IEnumerable<IItem> ItemsInHolsters()
        {
            yield return NonTargetItem();
            yield return NonTargetItem();
            yield return NonTargetItem();
        }

        protected override IEnumerable<IItem> ItemsInInventory()
        {
            yield return NonTargetItem();
            yield return NonTargetItem();
            yield return NonTargetItem();
        }
    }
}
