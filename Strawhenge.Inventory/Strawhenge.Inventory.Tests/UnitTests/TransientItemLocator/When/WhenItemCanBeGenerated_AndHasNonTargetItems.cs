using Strawhenge.Inventory.Items;
using System.Collections.Generic;

namespace Strawhenge.Inventory.Tests.UnitTests.TransientItemLocatorTests
{
    public class WhenItemCanBeGenerated_AndHasNonTargetItems : TransientItemLocator_Tests
    {
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
