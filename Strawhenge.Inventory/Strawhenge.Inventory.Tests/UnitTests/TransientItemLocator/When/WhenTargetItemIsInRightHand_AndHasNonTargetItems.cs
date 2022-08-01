using System.Collections.Generic;

namespace Strawhenge.Inventory.Tests.UnitTests.TransientItemLocatorTests
{
    public class WhenTargetItemIsInRightHand_AndHasNonTargetItems: WhenTargetItemIsInRightHand
    {
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
