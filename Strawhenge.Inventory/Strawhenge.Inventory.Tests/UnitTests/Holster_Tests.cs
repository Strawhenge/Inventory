using Moq;
using Strawhenge.Inventory.Containers;
using System;
using Xunit;

namespace Strawhenge.Inventory.Tests.UnitTests
{
    public class Holster_Tests
    {
        const string HolsterName = "Test Holster";

        readonly ItemContainer _itemContainer;
        readonly IItem _item;
        readonly IItem _otherItem;

        public Holster_Tests()
        {
            _itemContainer = new ItemContainer(HolsterName);

            _item = new Mock<IItem>().Object;
            _otherItem = new Mock<IItem>().Object;
        }

        [Fact]
        public void Init_CurrentItemShouldBeNone()
        {
            var currentItem = _itemContainer.CurrentItem;

            Assert.NotNull(currentItem);
            AssertMaybe.IsNone(currentItem);
        }

        [Fact]
        public void SetItem_CurrentItemShouldBeItem()
        {
            _itemContainer.SetItem(_item);

            var currentItem = _itemContainer.CurrentItem;

            Assert.NotNull(currentItem);
            AssertMaybe.IsSome(currentItem, expectedValue: _item);
        }

        [Fact]
        public void SetItem_ShouldThrowArgumentNullException_WhenPassingNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => _itemContainer.SetItem(null));
        }

        [Fact]
        public void UnsetItem_CurrentItemShouldBeNone_WhenItemNotSet()
        {
            _itemContainer.UnsetItem();

            var currentItem = _itemContainer.CurrentItem;

            Assert.NotNull(currentItem);
            AssertMaybe.IsNone(currentItem);
        }

        [Fact]
        public void UnsetItem_CurrentItemShouldBeNone_WhenItemSet()
        {
            _itemContainer.SetItem(_item);
            _itemContainer.UnsetItem();

            var currentItem = _itemContainer.CurrentItem;

            Assert.NotNull(currentItem);
            AssertMaybe.IsNone(currentItem);
        }

        [Fact]
        public void IsCurrentItem_ShouldBeFalse_WhenCurrentItemIsNone()
        {
            Assert.False(
                _itemContainer.IsCurrentItem(_item));
        }

        [Fact]
        public void IsCurrentItem_ShouldBeTrue_WhenCurrentItemIsSet()
        {
            _itemContainer.SetItem(_item);

            Assert.True(
                _itemContainer.IsCurrentItem(_item));
        }

        [Fact]
        public void IsCurrentItem_ShouldBeFalse_WhenCurrentItemIsOtherItem()
        {
            _itemContainer.SetItem(_otherItem);

            Assert.False(
                _itemContainer.IsCurrentItem(_item));
        }
    }
}