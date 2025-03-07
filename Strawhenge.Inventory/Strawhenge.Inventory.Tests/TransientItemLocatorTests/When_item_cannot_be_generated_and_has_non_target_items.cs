using System.Collections.Generic;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.TransientItemLocatorTests
{
    public class When_item_cannot_be_generated_and_has_non_target_items : BaseTransientItemLocatorTest
    {
        public When_item_cannot_be_generated_and_has_non_target_items(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override bool GetItemByName_ShouldReturnTargetItem => false;

        protected override Item ItemInLeftHand => NonTargetItem();

        protected override Item ItemInRightHand => NonTargetItem();

        protected override IEnumerable<(string holsterName, Item item)> ItemsInHolsters()
        {
            yield return (LeftHipHolster, NonTargetItem());
            yield return (RightHipHolster, NonTargetItem());
            yield return (BackHolster, NonTargetItem());
        }

        protected override IEnumerable<Item> ItemsInStorage()
        {
            yield return NonTargetItem();
            yield return NonTargetItem();
            yield return NonTargetItem();
        }
    }
}
