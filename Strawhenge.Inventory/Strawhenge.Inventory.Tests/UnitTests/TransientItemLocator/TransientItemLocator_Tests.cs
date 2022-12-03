using Moq;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.TransientItems;
using System.Collections.Generic;
using Xunit;

namespace Strawhenge.Inventory.Tests.UnitTests.TransientItemLocatorTests
{
    public abstract class TransientItemLocator_Tests
    {
        const string TargetItemName = "Test Item";

        readonly TransientItemLocator _sut;
        readonly Mock<IItem> _targetItemMock;

        ClearFromHandsPreference _clearFromHandsPreference;

        public TransientItemLocator_Tests()
        {
            _targetItemMock = new Mock<IItem>();
            _targetItemMock.SetupGet(x => x.Name).Returns(TargetItemName);

            _targetItemMock
                .SetupSet(x => x.ClearFromHandsPreference = It.IsAny<ClearFromHandsPreference>())
                .Callback<ClearFromHandsPreference>(x => _clearFromHandsPreference = x);

            var equippedItemsMock = new Mock<IEquippedItems>();
            equippedItemsMock.SetupNone(x => x.GetItemInLeftHand());
            equippedItemsMock.SetupNone(x => x.GetItemInRightHand());

            var inventoryMock = new Mock<IItemInventory>();

            var itemGeneratorMock = new Mock<IItemGenerator>();
            itemGeneratorMock.SetupNone(x => x.GenerateByName(TargetItemName));

            _sut = new TransientItemLocator(
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
                itemGeneratorMock.SetupSome(x => x.GenerateByName(TargetItemName), generatedItem);
        }

        protected IItem TargetItem => _targetItemMock.Object;

        protected abstract bool GetItemByName_ShouldReturnTargetItem { get; }

        protected virtual IItem ItemInLeftHand => null;

        protected virtual IItem ItemInRightHand => null;

        protected virtual ClearFromHandsPreference ExpectedClearFromHandsPreference => null;

        [Fact]
        public void GetItemByName()
        {
            var result = _sut.GetItemByName(TargetItemName);

            if (!GetItemByName_ShouldReturnTargetItem)
            {
                AssertMaybe.IsNone(result);
                return;
            }

            AssertMaybe.IsSome(result, _targetItemMock.Object);

            if (ExpectedClearFromHandsPreference is ClearFromHandsPreference expected)
            {
                Assert.IsType(expected.GetType(), _clearFromHandsPreference);
            }
            else
            {
                _targetItemMock.VerifySet(
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