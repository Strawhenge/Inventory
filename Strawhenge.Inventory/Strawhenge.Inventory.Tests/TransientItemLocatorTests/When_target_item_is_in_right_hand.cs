using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.TransientItemLocatorTests
{
    public class When_target_item_is_in_right_hand : BaseTransientItemLocatorTest
    {
        public When_target_item_is_in_right_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override bool GetItemByName_ShouldReturnTargetItem => true;

        protected override Item GenerateItem() => NonTargetItem(name: TargetItem.Name);

        protected override Item ItemInRightHand => TargetItem;
    }
}
