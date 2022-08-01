using Moq;
using Strawhenge.Inventory.Containers;
using System;
using Xunit;

namespace Strawhenge.Inventory.Tests.UnitTests
{
    public class Holster_Tests
    {
        const string holsterName = "Test Holster";

        readonly Holster holster;
        readonly IItem item;
        readonly IItem otherItem;

        public Holster_Tests()
        {
            holster = new Holster(holsterName);

            item = new Mock<IItem>().Object;
            otherItem = new Mock<IItem>().Object;
        }

        [Fact]
        public void Init_CurrentItemShouldBeNone()
        {
            var currentItem = holster.CurrentItem;

            Assert.NotNull(currentItem);
            AssertMaybe.IsNone(currentItem);
        }

        [Fact]
        public void SetItem_CurrentItemShouldBeItem()
        {
            holster.SetItem(item);

            var currentItem = holster.CurrentItem;

            Assert.NotNull(currentItem);
            AssertMaybe.IsSome(currentItem, expectedValue: item);
        }

        [Fact]
        public void SetItem_ShouldThrowArgumentNullException_WhenPassingNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => holster.SetItem(null));
        }

        [Fact]
        public void UnsetItem_CurrentItemShouldBeNone_WhenItemNotSet()
        {
            holster.UnsetItem();

            var currentItem = holster.CurrentItem;

            Assert.NotNull(currentItem);
            AssertMaybe.IsNone(currentItem);
        }

        [Fact]
        public void UnsetItem_CurrentItemShouldBeNone_WhenItemSet()
        {
            holster.SetItem(item);
            holster.UnsetItem();

            var currentItem = holster.CurrentItem;

            Assert.NotNull(currentItem);
            AssertMaybe.IsNone(currentItem);
        }

        [Fact]
        public void IsCurrentItem_ShouldBeFalse_WhenCurrentItemIsNone()
        {
            Assert.False(
                holster.IsCurrentItem(item));
        }

        [Fact]
        public void IsCurrentItem_ShouldBeTrue_WhenCurrentItemIsSet()
        {
            holster.SetItem(item);

            Assert.True(
                holster.IsCurrentItem(item));
        }

        [Fact]
        public void IsCurrentItem_ShouldBeFalse_WhenCurrentItemIsOtherItem()
        {
            holster.SetItem(otherItem);

            Assert.False(
                holster.IsCurrentItem(item));
        }
    }
}
