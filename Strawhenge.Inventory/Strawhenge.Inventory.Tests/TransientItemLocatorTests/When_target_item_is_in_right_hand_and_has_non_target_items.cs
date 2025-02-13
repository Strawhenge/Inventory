using System.Collections.Generic;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.TransientItemLocatorTests
{
    public class When_target_item_is_in_right_hand_and_has_non_target_items : When_target_item_is_in_right_hand
    {
        public When_target_item_is_in_right_hand_and_has_non_target_items(ITestOutputHelper testOutputHelper) : base(
            testOutputHelper)
        {
        }

        protected override IItem ItemInLeftHand => NonTargetItem();

        protected override IEnumerable<(string holsterName, IItem item)> ItemsInHolsters()
        {
            yield return (LeftHipHolster, NonTargetItem());
            yield return (RightHipHolster, NonTargetItem(name: TargetItem.Name));
            yield return (BackHolster, NonTargetItem());
        }

        protected override IEnumerable<IItem> ItemsInStorage()
        {
            yield return NonTargetItem();
            yield return NonTargetItem(name: TargetItem.Name);
            yield return NonTargetItem();
        }
    }
}