using System.Collections.Generic;

namespace Strawhenge.Inventory.Tests.UnitTests.TransientItemLocatorTests
{
    public class WhenTargetItemIsInLeftHand_AndHasNonTargetItems : WhenTargetItemIsInLeftHand
    {
        protected override IItem ItemInRightHand => NonTargetItem();

        protected override IEnumerable<IItem> ItemsInHolsters()
        {
            yield return NonTargetItem(name: TargetItem.Name);
            yield return NonTargetItem();
            yield return NonTargetItem();
        }

        protected override IEnumerable<IItem> ItemsInInventory()
        {
            yield return NonTargetItem();
            yield return NonTargetItem();
            yield return NonTargetItem(name: TargetItem.Name);
        }
    }
}
