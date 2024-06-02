using System;
using System.Collections.Generic;
using Moq;
using Strawhenge.Inventory.Items.HolsterForItem;
using Xunit;

namespace Strawhenge.Inventory.Tests.UnitTests
{
    public class HolstersForItem_Tests
    {
        readonly Lazy<HolstersForItem> _holsterForItems;
        readonly List<IHolsterForItem> _holsters;

        public HolstersForItem_Tests()
        {
            _holsters = new List<IHolsterForItem>();

            _holsterForItems = new Lazy<HolstersForItem>(
                () => new HolstersForItem(_holsters));
        }

        [Fact]
        public void IsEquippedToHolster_ShouldReturnFalse_WhenEmpty()
        {
            Assert.False(
                _holsterForItems.Value.IsEquippedToHolster(out IHolsterForItem holsterItem));

            Assert.Null(holsterItem);
        }

        [Fact]
        public void IsEquippedToHolster_ShouldReturnFalse_WhenAllHolsterUnequipped()
        {
            ArrangeHolster(equipped: false);
            ArrangeHolster(equipped: false);
            ArrangeHolster(equipped: false);

            Assert.False(
                _holsterForItems.Value.IsEquippedToHolster(out IHolsterForItem holsterItem));

            Assert.Null(holsterItem);
        }

        [Fact]
        public void IsEquippedToHolster_ShouldReturnTrue_WhenHolsterEquipped()
        {
            ArrangeHolster(equipped: false);
            var expectedHolster = ArrangeHolster(equipped: true);
            ArrangeHolster(equipped: false);

            Assert.True(
                _holsterForItems.Value.IsEquippedToHolster(out IHolsterForItem holsterItem));

            Assert.Same(expectedHolster, holsterItem);
        }

        IHolsterForItem ArrangeHolster(bool equipped)
        {
            var holsterMock = new Mock<IHolsterForItem>();
            holsterMock.SetupGet(x => x.IsEquipped).Returns(equipped);

            _holsters.Add(holsterMock.Object);

            return holsterMock.Object;
        }
    }
}