using Moq;
using Strawhenge.Inventory.Containers;
using System;
using Xunit;

namespace Strawhenge.Inventory.Tests.UnitTests
{
    public class Holster_Tests
    {
        const string HolsterName = "Test Holster";

        readonly Holster _holster;
        readonly IItem _item;
        readonly IItem _otherItem;

        public Holster_Tests()
        {
            _holster = new Holster(HolsterName);

            _item = new Mock<IItem>().Object;
            _otherItem = new Mock<IItem>().Object;
        }

        [Fact]
        public void Init_CurrentItemShouldBeNone()
        {
            var currentItem = _holster.CurrentItem;

            Assert.NotNull(currentItem);
            AssertMaybe.IsNone(currentItem);
        }

        [Fact]
        public void SetItem_CurrentItemShouldBeItem()
        {
            _holster.SetItem(_item);

            var currentItem = _holster.CurrentItem;

            Assert.NotNull(currentItem);
            AssertMaybe.IsSome(currentItem, expectedValue: _item);
        }

        [Fact]
        public void SetItem_ShouldThrowArgumentNullException_WhenPassingNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => _holster.SetItem(null));
        }

        [Fact]
        public void UnsetItem_CurrentItemShouldBeNone_WhenItemNotSet()
        {
            _holster.UnsetItem();

            var currentItem = _holster.CurrentItem;

            Assert.NotNull(currentItem);
            AssertMaybe.IsNone(currentItem);
        }

        [Fact]
        public void UnsetItem_CurrentItemShouldBeNone_WhenItemSet()
        {
            _holster.SetItem(_item);
            _holster.UnsetItem();

            var currentItem = _holster.CurrentItem;

            Assert.NotNull(currentItem);
            AssertMaybe.IsNone(currentItem);
        }

        [Fact]
        public void IsCurrentItem_ShouldBeFalse_WhenCurrentItemIsNone()
        {
            Assert.False(
                _holster.IsCurrentItem(_item));
        }

        [Fact]
        public void IsCurrentItem_ShouldBeTrue_WhenCurrentItemIsSet()
        {
            _holster.SetItem(_item);

            Assert.True(
                _holster.IsCurrentItem(_item));
        }

        [Fact]
        public void IsCurrentItem_ShouldBeFalse_WhenCurrentItemIsOtherItem()
        {
            _holster.SetItem(_otherItem);

            Assert.False(
                _holster.IsCurrentItem(_item));
        }
    }
}