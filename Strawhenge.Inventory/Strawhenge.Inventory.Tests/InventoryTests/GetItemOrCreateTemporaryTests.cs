using Strawhenge.Inventory.Items;
using Xunit;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.InventoryTests
{
    public class GetItemOrCreateTemporaryTests
    {
        const string Hammer = "Hammer";
        const string RightHip = "RightHip";
        const string LeftHip = "LeftHip";

        readonly Inventory _inventory;
        readonly Item _hammer;

        public GetItemOrCreateTemporaryTests(ITestOutputHelper testOutputHelper)
        {
            var inventoryContext = new InventoryTestContext(testOutputHelper);
            inventoryContext.AddHolster(LeftHip);
            inventoryContext.AddHolster(RightHip);
            _inventory = inventoryContext.Inventory;

            _hammer = ItemBuilder
                .Create(Hammer, ItemSize.OneHanded, isStorable: true, 0)
                .AddHolster(LeftHip)
                .AddHolster(RightHip)
                .Build();
        }

        [Fact]
        public void Should_return_temporary_item_when_item_not_in_inventory()
        {
            var item = _inventory.GetItemOrCreateTemporary(_hammer);

            Assert.True(item.IsTemporary);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Should_return_item_when_item_in_hand(bool left)
        {
            var expectedItem = ArrangeItemInHand(left);

            var item = _inventory.GetItemOrCreateTemporary(_hammer);

            Assert.Same(expectedItem, item);
        }

        [Fact]
        public void Should_return_item_when_item_in_holster()
        {
            var expectedItem = ArrangeItemInHolster(LeftHip);

            var item = _inventory
                .GetItemOrCreateTemporary(_hammer);

            Assert.Same(expectedItem, item);
        }

        [Fact]
        public void Should_return_item_from_hand_when_other_item_in_holster()
        {
            var expectedItem = ArrangeItemInHand();
            ArrangeItemInHolster(LeftHip);

            var item = _inventory.GetItemOrCreateTemporary(_hammer);

            Assert.Same(expectedItem, item);
        }

        [Fact]
        public void Should_return_item_when_item_in_storage()
        {
            var expectedItem = ArrangeItemInStorage();

            var item = _inventory.GetItemOrCreateTemporary(_hammer);

            Assert.Same(expectedItem, item);
        }

        [Fact]
        public void Should_return_item_from_hand_when_other_item_in_storage()
        {
            var expectedItem = ArrangeItemInHand();
            ArrangeItemInStorage();

            var item = _inventory.GetItemOrCreateTemporary(_hammer);

            Assert.Same(expectedItem, item);
        }

        [Fact]
        public void Should_return_item_from_holster_when_other_item_in_storage()
        {
            var expectedItem = ArrangeItemInHolster(LeftHip);
            ArrangeItemInStorage();

            var item = _inventory
                .GetItemOrCreateTemporary(_hammer);

            Assert.Same(expectedItem, item);
        }

        InventoryItem ArrangeItemInStorage()
        {
            var item = _inventory.CreateItem(_hammer);

            item.Storable
                .Reduce(() => throw new TestSetupException("Item not storable."))
                .AddToStorage();

            if (!item.IsInStorage)
                throw new TestSetupException("Item was not stored.");

            return item;
        }

        InventoryItem ArrangeItemInHolster(string holsterName)
        {
            var item = _inventory.CreateItem(_hammer);

            item.Holsters[holsterName]
                .Reduce(() =>
                    throw new TestSetupException($"Item not assignable to holster '{holsterName}'."))
                .Equip();

            return item;
        }

        InventoryItem ArrangeItemInHand(bool left = false)
        {
            var item = _inventory.CreateItem(_hammer);

            if (left)
                item.HoldLeftHand();
            else
                item.HoldRightHand();

            return item;
        }
    }
}