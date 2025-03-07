using System.Collections.Generic;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.TransientItemLocatorTests
{
    public class When_target_item_is_in_holster : BaseTransientItemLocatorTest
    {
        public When_target_item_is_in_holster(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override bool GetItemByName_ShouldReturnTargetItem => true;

        protected override Item GenerateItem() => NonTargetItem(name: TargetItem.Name);

        protected override Item ItemInLeftHand => NonTargetItem();

        protected override Item ItemInRightHand => NonTargetItem();

        protected override IEnumerable<(string holsterName, Item item)> ItemsInHolsters()
        {
            yield return (LeftHipHolster, NonTargetItem());
            yield return (RightHipHolster, TargetItem);
            yield return (BackHolster, NonTargetItem());
        }

        protected override IEnumerable<Item> ItemsInStorage()
        {
            yield return NonTargetItem();
            yield return NonTargetItem(name: TargetItem.Name);
            yield return NonTargetItem();
        }
    }
}