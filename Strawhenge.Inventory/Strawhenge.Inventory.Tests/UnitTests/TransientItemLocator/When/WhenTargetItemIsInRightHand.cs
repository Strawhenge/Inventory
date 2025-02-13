namespace Strawhenge.Inventory.Tests.UnitTests.TransientItemLocatorTests
{
    public class WhenTargetItemIsInRightHand : BaseTransientItemLocatorTest
    {
        protected override bool GetItemByName_ShouldReturnTargetItem => true;

        protected override IItem GenerateItem() => NonTargetItem(name: TargetItem.Name);

        protected override IItem ItemInRightHand => TargetItem;
    }
}
