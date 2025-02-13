using System.Collections.Generic;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.UnitTests.TransientItemLocatorTests
{
    public class WhenTargetItemIsInLeftHand_AndHasNonTargetItems : WhenTargetItemIsInLeftHand
    {
        public WhenTargetItemIsInLeftHand_AndHasNonTargetItems(ITestOutputHelper testOutputHelper) : base(
            testOutputHelper)
        {
        }

        protected override IItem ItemInRightHand => NonTargetItem();

        protected override IEnumerable<(string holsterName, IItem item)> ItemsInHolsters()
        {
            yield return (LeftHipHolster, NonTargetItem(name: TargetItem.Name));
            yield return (RightHipHolster, NonTargetItem());
            yield return (BackHolster, NonTargetItem());
        }

        protected override IEnumerable<IItem> ItemsInStorage()
        {
            yield return NonTargetItem();
            yield return NonTargetItem();
            yield return NonTargetItem(name: TargetItem.Name);
        }
    }
}