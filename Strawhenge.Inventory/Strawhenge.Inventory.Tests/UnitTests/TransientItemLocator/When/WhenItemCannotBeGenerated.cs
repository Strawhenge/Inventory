using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.UnitTests.TransientItemLocatorTests
{
    public class WhenItemCannotBeGenerated : BaseTransientItemLocatorTest
    {
        protected WhenItemCannotBeGenerated(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override bool GetItemByName_ShouldReturnTargetItem => false;
    }
}
