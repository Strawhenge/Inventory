using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.TransientItemLocatorTests
{
    public class When_item_cannot_be_generated : BaseTransientItemLocatorTest
    {
        public When_item_cannot_be_generated(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override bool GetItemByName_ShouldReturnTargetItem => false;
    }
}
