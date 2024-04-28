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
        readonly Mock<IEquippedItems> _equippedItemsMock;
        readonly Mock<IStoredItems> _inventoryMock;
        readonly Mock<IItemGenerator> _itemGeneratorMock;

        ClearFromHandsPreference _clearFromHandsPreference;

        protected TransientItemLocator_Tests()
        {
            _targetItemMock = new Mock<IItem>();
            _targetItemMock.SetupGet(x => x.Name).Returns(TargetItemName);

            _targetItemMock
                .SetupSet(x => x.ClearFromHandsPreference = It.IsAny<ClearFromHandsPreference>())
                .Callback<ClearFromHandsPreference>(x => _clearFromHandsPreference = x);

            _equippedItemsMock = new Mock<IEquippedItems>();
            _equippedItemsMock.SetupNone(x => x.GetItemInLeftHand());
            _equippedItemsMock.SetupNone(x => x.GetItemInRightHand());

            _inventoryMock = new Mock<IStoredItems>();

            _itemGeneratorMock = new Mock<IItemGenerator>();
            _itemGeneratorMock.SetupNone(x => x.GenerateByName(TargetItemName));

            _sut = new TransientItemLocator(
                _equippedItemsMock.Object,
                _inventoryMock.Object,
                _itemGeneratorMock.Object);
        }

        protected IItem TargetItem => _targetItemMock.Object;

        protected abstract bool GetItemByName_ShouldReturnTargetItem { get; }

        protected virtual IItem ItemInLeftHand => null;

        protected virtual IItem ItemInRightHand => null;

        protected virtual ClearFromHandsPreference ExpectedClearFromHandsPreference => null;

        [Fact]
        public void GetItemByName()
        {
            ArrangeMocks();

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

        void ArrangeMocks()
        {
            if (ItemInLeftHand is IItem left)
                _equippedItemsMock.SetupSome(x => x.GetItemInLeftHand(), left);

            if (ItemInRightHand is IItem right)
                _equippedItemsMock.SetupSome(x => x.GetItemInRightHand(), right);

            if (ItemsInHolsters() is IEnumerable<IItem> holster)
                _equippedItemsMock.Setup(x => x.GetItemsInHolsters()).Returns(holster);

            if (ItemsInInventory() is IEnumerable<IItem> inventory)
                _inventoryMock.SetupGet(x => x.AllItems).Returns(inventory);

            if (GenerateItem() is IItem generatedItem)
                _itemGeneratorMock.SetupSome(x => x.GenerateByName(TargetItemName), generatedItem);
        }
    }
}