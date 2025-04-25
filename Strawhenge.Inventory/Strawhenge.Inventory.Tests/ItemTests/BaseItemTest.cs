using System;
using System.Collections.Generic;
using System.Linq;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests
{
    public abstract class BaseItemTest
    {
        protected const string Hammer = "Hammer";
        protected const string Knife = "Knife";
        protected const string Spear = "Spear";

        protected const string LeftHipHolster = "Left Hip";
        protected const string RightHipHolster = "Right Hip";
        protected const string BackHolster = "Back";

        protected const string Show = "Show";
        protected const string Hide = "Hide";
        protected const string AppearRightHand = "AppearRightHand";
        protected const string DropRightHand = "DropRightHand";
        protected const string PutAwayRightHand = "PutAwayRightHand";
        protected const string DisappearRightHand = "DisappearRightHand";
        protected const string DrawRightHand = "DrawRightHand";
        protected const string SpawnAndDrop = "SpawnAndDrop";
        protected const string DrawLeftHand = "DrawLeftHand";
        protected const string PutAwayLeftHand = "PutAwayLeftHand";
        protected const string DisappearLeftHand = "DisappearLeftHand";
        protected const string AppearLeftHand = "AppearLeftHand";
        protected const string RightHandToLeftHand = "RightHandToLeftHand";
        protected const string Drop = "Drop";

        readonly InventoryTestContext _inventoryContext;

        protected BaseItemTest(ITestOutputHelper testOutputHelper)
        {
            _inventoryContext = new InventoryTestContext(testOutputHelper);
            Inventory = _inventoryContext.Inventory;
        }

        [Fact]
        public void Verify_item_in_left_hand()
        {
            if (ExpectedItemInLeftHand.HasSome(out var expectedItem))
                Inventory.Hands.LeftHand.CurrentItem.VerifyIsSome(expectedItem);
            else
                Inventory.Hands.LeftHand.CurrentItem.VerifyIsNone();
        }

        [Fact]
        public void Verify_item_in_right_hand()
        {
            if (ExpectedItemInRightHand.HasSome(out var expectedItem))
                Inventory.Hands.RightHand.CurrentItem.VerifyIsSome(expectedItem);
            else
                Inventory.Hands.RightHand.CurrentItem.VerifyIsNone();
        }

        [Fact]
        public void Verify_items_in_holsters()
        {
            var expectedItems = ExpectedItemsInHolsters()
                .OrderBy(x => x.holsterName)
                .ToArray();

            var actualItems = Inventory.Holsters
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

            var actualItems = Inventory.StoredItems.Items
                .OrderBy(x => x.Name)
                .ToArray();

            Assert.Equal(expectedItems, actualItems);
        }

        [Fact]
        public void Verify_procedures_completed()
        {
            _inventoryContext
                .VerifyProcedures(ExpectedProceduresCompleted().ToArray());
        }

        protected Inventory Inventory { get; }

        protected void AddHolster(string name) => _inventoryContext.AddHolster(name);

        protected void SetStorageCapacity(int capacity) =>
            _inventoryContext.SetStorageCapacity(capacity);

        protected Item CreateItem(string name, bool storable = false) =>
            CreateItem(name, Array.Empty<string>(), storable);

        protected Item CreateItem(string name, string[] holsterNames, bool storable = false) =>
            _inventoryContext.CreateItem(name, ItemSize.OneHanded, holsterNames, storable);

        protected Item CreateTwoHandedItem(string name, bool storable = false) =>
            CreateTwoHandedItem(name, Array.Empty<string>(), storable);

        protected Item CreateTwoHandedItem(string name, string[] holsterNames, bool storable = false) =>
            _inventoryContext.CreateItem(name, ItemSize.TwoHanded, holsterNames, storable);

        protected Item CreateTransientItem(string name) =>
            _inventoryContext.CreateTransientItem(name);

        protected virtual Maybe<Item> ExpectedItemInLeftHand => Maybe.None<Item>();

        protected virtual Maybe<Item> ExpectedItemInRightHand => Maybe.None<Item>();

        protected virtual IEnumerable<(string holsterName, Item expectedItem)> ExpectedItemsInHolsters() =>
            Enumerable.Empty<(string holsterName, Item expectedItem)>();

        protected virtual IEnumerable<Item> ExpectedItemsInStorage() => Enumerable.Empty<Item>();

        protected abstract IEnumerable<ProcedureInfo> ExpectedProceduresCompleted();
    }
}