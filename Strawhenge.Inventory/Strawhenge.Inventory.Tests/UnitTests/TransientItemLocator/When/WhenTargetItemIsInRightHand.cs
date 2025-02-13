using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.UnitTests.TransientItemLocatorTests
{
    public class WhenTargetItemIsInRightHand : BaseTransientItemLocatorTest
    {
        protected WhenTargetItemIsInRightHand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override bool GetItemByName_ShouldReturnTargetItem => true;

        protected override IItem GenerateItem() => NonTargetItem(name: TargetItem.Name);

        protected override IItem ItemInRightHand => TargetItem;
    }
}
