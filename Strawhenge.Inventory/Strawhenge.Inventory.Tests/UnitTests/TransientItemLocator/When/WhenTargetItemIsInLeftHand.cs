namespace Strawhenge.Inventory.Tests.UnitTests.TransientItemLocatorTests
{
    public class WhenTargetItemIsInLeftHand : BaseTransientItemLocatorTest
    {
        protected override bool GetItemByName_ShouldReturnTargetItem => true;

        protected override IItem GenerateItem() => NonTargetItem(name: TargetItem.Name);

        protected override IItem ItemInLeftHand => TargetItem;
    }
}
