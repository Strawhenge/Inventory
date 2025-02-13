using System.Collections.Generic;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.UnitTests.TransientItemLocatorTests
{
    public class WhenTargetItemIsInRightHand_AndHasNonTargetItems: WhenTargetItemIsInRightHand
    {
        protected WhenTargetItemIsInRightHand_AndHasNonTargetItems(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IItem ItemInLeftHand => NonTargetItem();

        protected override IEnumerable<IItem> ItemsInHolsters()
        {
            yield return NonTargetItem();
            yield return NonTargetItem(name: TargetItem.Name);
            yield return NonTargetItem();
        }

        protected override IEnumerable<IItem> ItemsInInventory()
        {
            yield return NonTargetItem();
            yield return NonTargetItem(name: TargetItem.Name);
            yield return NonTargetItem();
        }
    }
}
