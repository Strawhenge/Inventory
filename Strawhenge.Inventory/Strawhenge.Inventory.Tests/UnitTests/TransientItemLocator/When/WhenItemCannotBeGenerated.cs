namespace Strawhenge.Inventory.Tests.UnitTests.TransientItemLocatorTests
{
    public class WhenItemCannotBeGenerated : TransientItemLocator_Tests
    {
        protected override bool GetItemByName_ShouldReturnTargetItem => false;
    }
}
