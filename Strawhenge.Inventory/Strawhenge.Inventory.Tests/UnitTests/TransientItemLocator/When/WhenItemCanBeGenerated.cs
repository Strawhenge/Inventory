using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory.Tests.UnitTests.TransientItemLocatorTests
{
    public class WhenItemCanBeGenerated : BaseTransientItemLocatorTest
    {
        protected override bool GetItemByName_ShouldReturnTargetItem => true;

        protected override IItem GenerateItem() => TargetItem;

        protected override ClearFromHandsPreference ExpectedClearFromHandsPreference => ClearFromHandsPreference.Disappear;
    }
}
