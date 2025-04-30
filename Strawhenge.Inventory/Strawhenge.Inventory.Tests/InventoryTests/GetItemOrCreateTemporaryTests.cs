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

        readonly InventoryTestContext _inventoryContext;
        readonly Inventory _inventory;
        readonly ItemData _hammer;

        public GetItemOrCreateTemporaryTests(ITestOutputHelper testOutputHelper)
        {
            _inventoryContext = new InventoryTestContext(testOutputHelper);
            _inventoryContext.AddHolster(LeftHip);
            _inventoryContext.AddHolster(RightHip);
            _inventory = _inventoryContext.Inventory;

            _hammer = ItemDataBuilder
                .Create(Hammer, ItemSize.OneHanded, isStorable: true, 0)
                .AddHolster(LeftHip)
                .AddHolster(RightHip)
                .Build();
        }

        [Fact]
        public void Should_return_none_when_item_not_in_inventory_and_cannot_be_created()
        {
            var item = _inventory.GetItemOrCreateTemporary(Hammer);

            item.VerifyIsNone();
        }

        [Fact]
        public void Should_return_temporary_item_when_item_not_in_inventory_and_can_be_created()
        {
            ArrangeItemCanBeCreated();

            var item = _inventory
                .GetItemOrCreateTemporary(Hammer)
                .VerifyIsSome();

            Assert.True(item.IsTemporary);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Should_return_item_when_item_in_hand(bool left)
        {
            var expectedItem = ArrangeItemInHand(left);

            var item = _inventory
                .GetItemOrCreateTemporary(Hammer)
                .VerifyIsSome();

            Assert.Same(expectedItem, item);
        }

        [Fact]
        public void Should_return_item_when_item_in_holster()
        {
            var expectedItem = ArrangeItemInHolster(LeftHip);

            var item = _inventory
                .GetItemOrCreateTemporary(Hammer)
                .VerifyIsSome();

            Assert.Same(expectedItem, item);
        }

        [Fact]
        public void Should_return_item_from_hand_when_other_item_in_holster()
        {
            var expectedItem = ArrangeItemInHand();
            ArrangeItemInHolster(LeftHip);

            var item = _inventory
                .GetItemOrCreateTemporary(Hammer)
                .VerifyIsSome();

            Assert.Same(expectedItem, item);
        }

        [Fact]
        public void Should_return_item_when_item_in_storage()
        {
            var expectedItem = ArrangeItemInStorage();

            var item = _inventory
                .GetItemOrCreateTemporary(Hammer)
                .VerifyIsSome();

            Assert.Same(expectedItem, item);
        }

        [Fact]
        public void Should_return_item_from_hand_when_other_item_in_storage()
        {
            var expectedItem = ArrangeItemInHand();
            ArrangeItemInStorage();

            var item = _inventory
                .GetItemOrCreateTemporary(Hammer)
                .VerifyIsSome();

            Assert.Same(expectedItem, item);
        }

        [Fact]
        public void Should_return_item_from_holster_when_other_item_in_storage()
        {
            var expectedItem = ArrangeItemInHolster(LeftHip);
            ArrangeItemInStorage();

            var item = _inventory
                .GetItemOrCreateTemporary(Hammer)
                .VerifyIsSome();

            Assert.Same(expectedItem, item);
        }

        Item ArrangeItemInStorage()
        {
            var item = _inventory.CreateItem(_hammer);

            item.Storable
                .Reduce(() => throw new TestSetupException("Item not storable."))
                .AddToStorage();

            if (!item.IsInStorage)
                throw new TestSetupException("Item was not stored.");

            return item;
        }

        Item ArrangeItemInHolster(string holsterName)
        {
            var item = _inventory.CreateItem(_hammer);

            item.Holsters[holsterName]
                .Reduce(() =>
                    throw new TestSetupException($"Item not assignable to holster '{holsterName}'."))
                .Equip();

            return item;
        }

        Item ArrangeItemInHand(bool left = false)
        {
            var item = _inventory.CreateItem(_hammer);

            if (left)
                item.HoldLeftHand();
            else
                item.HoldRightHand();

            return item;
        }

        void ArrangeItemCanBeCreated()
        {
            _inventoryContext.AddToRepository(_hammer);
        }
    }
}