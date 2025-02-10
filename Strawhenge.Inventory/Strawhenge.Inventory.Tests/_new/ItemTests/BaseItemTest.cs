using System;
using System.Collections.Generic;
using System.Linq;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests._new
{
    public abstract class BaseItemTest
    {
        protected const string Hammer = "Hammer";
        protected const string Knife = "Knife";
        protected const string Spear = "Spear";
        protected const string LeftHipHolster = "Left Hip";
        protected const string RightHipHolster = "Right Hip";
        protected const string BackHolster = "Back";

        readonly InventoryTestContext _inventoryContext;
        readonly Inventory _inventory;

        protected BaseItemTest(ITestOutputHelper testOutputHelper)
        {
            _inventoryContext = new InventoryTestContext(testOutputHelper);
            _inventory = _inventoryContext.Inventory;
        }

        [Fact]
        public void Verify_item_in_left_hand()
        {
            if (ExpectedItemInLeftHand.HasSome(out var expectedItem))
                _inventory.LeftHand.CurrentItem.VerifyIsSome(expectedItem);
            else
                _inventory.LeftHand.CurrentItem.VerifyIsNone();
        }

        [Fact]
        public void Verify_item_in_right_hand()
        {
            if (ExpectedItemInRightHand.HasSome(out var expectedItem))
                _inventory.RightHand.CurrentItem.VerifyIsSome(expectedItem);
            else
                _inventory.RightHand.CurrentItem.VerifyIsNone();
        }

        [Fact]
        public void Verify_items_in_holsters()
        {
            var expectedItems = ExpectedItemsInHolsters()
                .OrderBy(x => x.holsterName)
                .ToArray();

            var actualItems = _inventory.Holsters
                .Select(x => x.CurrentItem.HasSome(out var item) ? (x.Name, item) : default)
                .Where(x => x != default)
                .OrderBy(x => x.Name)
                .ToArray();

            Assert.Equal(expectedItems, actualItems);
        }

        [Fact]
        public void Verify_items_in_storage()
        {
            var expectedItems = ExpectedItemsInStorage()
                .OrderBy(x => x.Name)
                .ToArray();

            var actualItems = _inventory.StoredItems.Items
                .OrderBy(x => x.Name)
                .ToArray();

            Assert.Equal(expectedItems, actualItems);
        }

        [Fact]
        public void Verify_view_calls()
        {
            _inventoryContext
                .VerifyViewCalls(ExpectedViewCalls().ToArray());
        }

        protected IInventory Inventory => _inventory;

        protected void AddHolster(string name) => _inventoryContext.AddHolster(name);

        protected void SetStorageCapacity(int capacity) =>
            _inventoryContext.StoredItemsWeightCapacity.SetWeightCapacity(capacity);

        protected Item CreateItem(string name, bool storable = false) =>
            CreateItem(name, Array.Empty<string>(), storable);

        protected Item CreateItem(string name, string[] holsterNames, bool storable = false) =>
            _inventoryContext.CreateItem(name, ItemSize.OneHanded, holsterNames, storable);

        protected Item CreateTwoHandedItem(string name, bool storable = false) =>
            CreateTwoHandedItem(name, Array.Empty<string>(), storable);

        protected Item CreateTwoHandedItem(string name, string[] holsterNames, bool storable = false) =>
            _inventoryContext.CreateItem(name, ItemSize.TwoHanded, holsterNames, storable);

        protected virtual Maybe<Item> ExpectedItemInLeftHand => Maybe.None<Item>();

        protected virtual Maybe<Item> ExpectedItemInRightHand => Maybe.None<Item>();

        protected virtual IEnumerable<(string holsterName, IItem expectedItem)> ExpectedItemsInHolsters() =>
            Enumerable.Empty<(string holsterName, IItem expectedItem)>();

        protected virtual IEnumerable<IItem> ExpectedItemsInStorage() => Enumerable.Empty<IItem>();

        protected abstract IEnumerable<ViewCallInfo> ExpectedViewCalls();
    }
}