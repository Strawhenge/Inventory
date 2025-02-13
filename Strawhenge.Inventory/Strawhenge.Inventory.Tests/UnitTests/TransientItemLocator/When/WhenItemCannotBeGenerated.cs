namespace Strawhenge.Inventory.Tests.UnitTests.TransientItemLocatorTests
{
    public class WhenItemCannotBeGenerated : BaseTransientItemLocatorTest
    {
        protected override bool GetItemByName_ShouldReturnTargetItem => false;
    }
}
