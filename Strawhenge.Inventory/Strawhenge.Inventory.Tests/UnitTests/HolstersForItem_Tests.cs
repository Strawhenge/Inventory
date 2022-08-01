using Moq;
using Strawhenge.Inventory.Items.HolsterForItem;
using System;
using System.Collections.Generic;
using Xunit;

namespace Strawhenge.Inventory.Tests.UnitTests
{
    public class HolstersForItem_Tests
    {
        readonly Lazy<HolstersForItem> holsterForItems;
        readonly List<IHolsterForItem> holsters;
        readonly Mock<ILogger> loggerMock;

        public HolstersForItem_Tests()
        {
            holsters = new List<IHolsterForItem>();
            loggerMock = new Mock<ILogger>();

            holsterForItems = new Lazy<HolstersForItem>(
                () => new HolstersForItem(holsters, loggerMock.Object));
        }

        [Fact]
        public void IsEquippedToHolster_ShouldReturnFalse_WhenEmpty()
        {
            Assert.False(
                holsterForItems.Value.IsEquippedToHolster(out IHolsterForItem holsterItem));
            
            Assert.Null(holsterItem);
        }

        [Fact]
        public void IsEquippedToHolster_ShouldReturnFalse_WhenAllHolsterUnequipped()
        {
            ArrangeHolster(equipped: false);
            ArrangeHolster(equipped: false);
            ArrangeHolster(equipped: false);

            Assert.False(
                holsterForItems.Value.IsEquippedToHolster(out IHolsterForItem holsterItem));

            Assert.Null(holsterItem);
        }

        [Fact]
        public void IsEquippedToHolster_ShouldReturnTrue_WhenOneHolsterEquipped()
        {
            ArrangeHolster(equipped: false);
            var expectedHolster = ArrangeHolster(equipped: true);
            ArrangeHolster(equipped: false);

            Assert.True(
                holsterForItems.Value.IsEquippedToHolster(out IHolsterForItem holsterItem));

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
                holsterForItems.Value.IsEquippedToHolster(out IHolsterForItem holsterItem));

            Assert.Contains(holsterItem, possibleExpectedHolsters);

            loggerMock.VerifyOnce(
                x => x.LogError(It.IsAny<string>()));
        }

        IHolsterForItem ArrangeHolster(bool equipped)
        {
            var holsterMock = new Mock<IHolsterForItem>();
            holsterMock.SetupGet(x => x.IsEquipped).Returns(equipped);

            holsters.Add(holsterMock.Object);

            return holsterMock.Object;
        }
    }
}
