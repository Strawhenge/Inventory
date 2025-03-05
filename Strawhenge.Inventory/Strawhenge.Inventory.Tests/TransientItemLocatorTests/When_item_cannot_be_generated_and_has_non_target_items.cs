using System.Collections.Generic;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.TransientItemLocatorTests
{
    public class When_item_cannot_be_generated_and_has_non_target_items : BaseTransientItemLocatorTest
    {
        public When_item_cannot_be_generated_and_has_non_target_items(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override bool GetItemByName_ShouldReturnTargetItem => false;

        protected override IItem ItemInLeftHand => NonTargetItem();

        protected override IItem ItemInRightHand => NonTargetItem();

        protected override IEnumerable<(string holsterName, IItem item)> ItemsInHolsters()
        {
            yield return (LeftHipHolster, NonTargetItem());
            yield return (RightHipHolster, NonTargetItem());
            yield return (BackHolster, NonTargetItem());
        }

        protected override IEnumerable<IItem> ItemsInStorage()
        {
            yield return NonTargetItem();
            yield return NonTargetItem();
            yield return NonTargetItem();
        }
    }
}
