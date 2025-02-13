using System.Collections.Generic;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.UnitTests.TransientItemLocatorTests
{
    public class WhenTargetItemIsInInventory : BaseTransientItemLocatorTest
    {
        protected WhenTargetItemIsInInventory(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override bool GetItemByName_ShouldReturnTargetItem => true;

        protected override IItem GenerateItem() => NonTargetItem(name: TargetItem.Name);

        protected override IItem ItemInLeftHand => NonTargetItem();

        protected override IItem ItemInRightHand => NonTargetItem();

        protected override IEnumerable<IItem> ItemsInHolsters()
        {
            yield return NonTargetItem();
            yield return NonTargetItem();
            yield return NonTargetItem();
        }

        protected override IEnumerable<IItem> ItemsInInventory()
        {
            yield return NonTargetItem();
            yield return TargetItem;
            yield return NonTargetItem();
        }
    }
}
