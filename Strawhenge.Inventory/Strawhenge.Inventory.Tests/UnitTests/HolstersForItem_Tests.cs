using Moq;
using Strawhenge.Inventory.Items.HolsterForItem;
using System;
using System.Collections.Generic;
using Strawhenge.Common.Logging;
using Xunit;

namespace Strawhenge.Inventory.Tests.UnitTests
{
    public class HolstersForItem_Tests
    {
        readonly Lazy<HolstersForItem> _holsterForItems;
        readonly List<IHolsterForItem> _holsters;
        readonly Mock<ILogger> _loggerMock;

        public HolstersForItem_Tests()
        {
            _holsters = new List<IHolsterForItem>();
            _loggerMock = new Mock<ILogger>();

            _holsterForItems = new Lazy<HolstersForItem>(
                () => new HolstersForItem(_holsters, _loggerMock.Object));
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
        public void IsEquippedToHolster_ShouldReturnTrue_WhenOneHolsterEquipped()
        {
            ArrangeHolster(equipped: false);
            var expectedHolster = ArrangeHolster(equipped: true);
            ArrangeHolster(equipped: false);

            Assert.True(
                _holsterForItems.Value.IsEquippedToHolster(out IHolsterForItem holsterItem));

            Assert.Same(expectedHolster, holsterItem);
        }

        [Fact]
        public void IsEquippedToHolster_ShouldReturnTrue_AndLogError_WhenMultipleHolstersEquipped()
        {
            ArrangeHolster(equipped: false);

            var possibleExpectedHolsters = new List<IHolsterForItem>
            {
                ArrangeHolster(equipped: true),
                ArrangeHolster(equipped: true)
            };

            ArrangeHolster(equipped: false);

            Assert.True(
                _holsterForItems.Value.IsEquippedToHolster(out IHolsterForItem holsterItem));

            Assert.Contains(holsterItem, possibleExpectedHolsters);

            _loggerMock.VerifyOnce(
                x => x.LogError(It.IsAny<string>()));
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