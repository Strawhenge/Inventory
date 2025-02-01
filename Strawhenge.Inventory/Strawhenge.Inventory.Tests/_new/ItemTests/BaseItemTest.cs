﻿using System;
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
        protected const string RightHip = "Right Hip";

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
                AssertMaybe.IsSome(_inventory.LeftHand.CurrentItem, expectedItem);
            else
                AssertMaybe.IsNone(_inventory.LeftHand.CurrentItem);
        }

        [Fact]
        public void Verify_item_in_right_hand()
        {
            if (ExpectedItemInRightHand.HasSome(out var expectedItem))
                AssertMaybe.IsSome(_inventory.RightHand.CurrentItem, expectedItem);
            else
                AssertMaybe.IsNone(_inventory.RightHand.CurrentItem);
        }

        [Fact]
        public void Verify_items_in_holsters()
        {
            var expectedItems = ExpectedItemsInHolsters().ToArray();

            var actualItems = _inventory.Holsters
                .Select(x => x.CurrentItem.HasSome(out var item) ? (x.Name, item) : default)
                .Where(x => x != default)
                .ToArray();

            Assert.Equal(expectedItems, actualItems);
        }

        [Fact]
        public void Verify_view_calls()
        {
            _inventoryContext
                .VerifyViewCalls(ExpectedViewCalls().ToArray());
        }

        protected void AddHolster(string name) => _inventoryContext.AddHolster(name);

        protected Item CreateItem(string name) => CreateItem(name, Array.Empty<string>());

        protected Item CreateItem(string name, string[] holsterNames) =>
            _inventoryContext.CreateItem(name, ItemSize.OneHanded, holsterNames);

        protected Item CreateTwoHandedItem(string name) => CreateTwoHandedItem(name, Array.Empty<string>());

        protected Item CreateTwoHandedItem(string name, string[] holsterNames) =>
            _inventoryContext.CreateItem(name, ItemSize.TwoHanded, holsterNames);

        protected virtual Maybe<Item> ExpectedItemInLeftHand => Maybe.None<Item>();

        protected virtual Maybe<Item> ExpectedItemInRightHand => Maybe.None<Item>();

        protected virtual IEnumerable<(string holsterName, IItem expectedItem)> ExpectedItemsInHolsters() =>
            Enumerable.Empty<(string holsterName, IItem expectedItem)>();

        protected abstract IEnumerable<ViewCallInfo> ExpectedViewCalls();
    }
}