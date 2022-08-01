using Moq;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.TransientItems;
using System.Collections.Generic;
using Xunit;

namespace Strawhenge.Inventory.Tests.UnitTests.TransientItemLocatorTests
{
    public abstract class TransientItemLocator_Tests
    {
        private const string targetItemName = "Test Item";

        private readonly TransientItemLocator sut;
        private readonly Mock<IItem> targetItemMock;
        private readonly Mock<IEquippedItems> equippedItemsMock;
        private readonly Mock<IItemInventory> inventoryMock;
        private readonly Mock<IItemGenerator> itemGeneratorMock;

        ClearFromHandsPreference clearFromHandsPreference;

        public TransientItemLocator_Tests()
        {
            targetItemMock = new Mock<IItem>();
            targetItemMock.SetupGet(x => x.Name).Returns(targetItemName);

            targetItemMock
                .SetupSet(x => x.ClearFromHandsPreference = It.IsAny<ClearFromHandsPreference>())
                .Callback<ClearFromHandsPreference>(x => clearFromHandsPreference = x);

            equippedItemsMock = new Mock<IEquippedItems>();
            equippedItemsMock.SetupNone(x => x.GetItemInLeftHand());
            equippedItemsMock.SetupNone(x => x.GetItemInRightHand());

            inventoryMock = new Mock<IItemInventory>();

            itemGeneratorMock = new Mock<IItemGenerator>();
            itemGeneratorMock.SetupNone(x => x.GenerateByName(targetItemName));

            sut = new TransientItemLocator(
                equippedItemsMock.Object,
                inventoryMock.Object,
                itemGeneratorMock.Object);

            if (ItemInLeftHand is IItem left)
                equippedItemsMock.SetupSome(x => x.GetItemInLeftHand(), left);

            if (ItemInRightHand is IItem right)
                equippedItemsMock.SetupSome(x => x.GetItemInRightHand(), right);

            if (ItemsInHolsters() is IEnumerable<IItem> holster)
                equippedItemsMock.Setup(x => x.GetItemsInHolsters()).Returns(holster);

            if (ItemsInInventory() is IEnumerable<IItem> inventory)
                inventoryMock.SetupGet(x => x.AllItems).Returns(inventory);

            if (GenerateItem() is IItem generatedItem)
                itemGeneratorMock.SetupSome(x => x.GenerateByName(targetItemName), generatedItem);
        }

        protected IItem TargetItem => targetItemMock.Object;

        protected abstract bool GetItemByName_ShouldReturnTargetItem { get; }

        protected virtual IItem ItemInLeftHand => null;

        protected virtual IItem ItemInRightHand => null;

        protected virtual ClearFromHandsPreference ExpectedClearFromHandsPreference => null;

        [Fact]
        public void GetItemByName()
        {
            var result = sut.GetItemByName(targetItemName);

            if (!GetItemByName_ShouldReturnTargetItem)
            {
                AssertMaybe.IsNone(result);
                return;
            }

            AssertMaybe.IsSome(result, targetItemMock.Object);

            if (ExpectedClearFromHandsPreference is ClearFromHandsPreference expected)
            {
                Assert.IsType(expected.GetType(), clearFromHandsPreference);
            }
            else
            {
                targetItemMock.VerifySet(
                    x => x.ClearFromHandsPreference = It.IsAny<ClearFromHandsPreference>(), Times.Never);
            }
        }

        protected virtual IEnumerable<IItem> ItemsInHolsters() => null;

        protected virtual IEnumerable<IItem> ItemsInInventory() => null;

        protected virtual IItem GenerateItem() => null;

        protected IItem NonTargetItem(string name = null)
        {
            var item = new Mock<IItem>();
            item.SetupGet(x => x.Name).Returns(name ?? "Other Item");
            return item.Object;
        }
    }
}