using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.UnitTests.TransientItemLocatorTests
{
    public class WhenTargetItemIsInLeftHand : BaseTransientItemLocatorTest
    {
        protected WhenTargetItemIsInLeftHand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override bool GetItemByName_ShouldReturnTargetItem => true;

        protected override IItem GenerateItem() => NonTargetItem(name: TargetItem.Name);

        protected override IItem ItemInLeftHand => TargetItem;
    }
}
