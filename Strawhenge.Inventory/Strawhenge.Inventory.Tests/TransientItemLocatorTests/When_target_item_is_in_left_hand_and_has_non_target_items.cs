using System.Collections.Generic;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.TransientItemLocatorTests
{
    public class When_target_item_is_in_left_hand_and_has_non_target_items : When_target_item_is_in_left_hand
    {
        public When_target_item_is_in_left_hand_and_has_non_target_items(ITestOutputHelper testOutputHelper) : base(
            testOutputHelper)
        {
        }

        protected override Item ItemInRightHand => NonTargetItem();

        protected override IEnumerable<(string holsterName, Item item)> ItemsInHolsters()
        {
            yield return (LeftHipHolster, NonTargetItem(name: TargetItem.Name));
            yield return (RightHipHolster, NonTargetItem());
            yield return (BackHolster, NonTargetItem());
        }

        protected override IEnumerable<Item> ItemsInStorage()
        {
            yield return NonTargetItem();
            yield return NonTargetItem();
            yield return NonTargetItem(name: TargetItem.Name);
        }
    }
}