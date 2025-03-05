using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.TransientItemLocatorTests
{
    public class When_item_can_be_generated : BaseTransientItemLocatorTest
    {
        public When_item_can_be_generated(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override bool GetItemByName_ShouldReturnTargetItem => true;

        protected override IItem GenerateItem() => TargetItem;

        protected override ClearFromHandsPreference ExpectedClearFromHandsPreference => ClearFromHandsPreference.Disappear;
    }
}
