﻿using System;
using System.Linq;
using FunctionalUtilities;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Items.HolsterForItem;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests._new
{
    class InventoryTestContext
    {
        readonly ViewCallsTracker _viewCallsTracker;
        readonly Hands _hands;
        readonly Holsters _holsters;
        readonly StoredItems _storedItems;
        readonly ApparelSlotsFake _apparelSlots;

        public InventoryTestContext(ITestOutputHelper testOutputHelper)
        {
            var logger = new TestOutputLogger(testOutputHelper);
            _viewCallsTracker = new ViewCallsTracker(logger);

            _storedItems = new StoredItems();
            _hands = new Hands();
            _holsters = new Holsters(logger);
            _apparelSlots = new ApparelSlotsFake();

            Inventory = new Inventory(
                _storedItems,
                _hands,
                _holsters,
                _apparelSlots);
        }

        public Inventory Inventory { get; }

        public IStoredItemsWeightCapacitySetter StoredItemsWeightCapacity => _storedItems;

        public void AddHolster(string name) => _holsters.Add(name);

        public void AddApparelSlot(string name) => _apparelSlots.Add(name);

        public Item CreateItem(string name, ItemSize size = null, string[] holsterNames = null, bool storable = false)
        {
            size ??= ItemSize.OneHanded;
            holsterNames ??= Array.Empty<string>();

            var itemView = new ItemViewFake(name);
            _viewCallsTracker.Track(itemView);

            var item = new Item(name, _hands, itemView, size);

            var holsters = holsterNames.Select(holsterName =>
            {
                var holsterForItemView = new HolsterForItemViewFake(name, holsterName);
                _viewCallsTracker.Track(holsterForItemView);

                var holster = _holsters
                    .FindByName(holsterName)
                    .Reduce(() => throw new TestSetupException($"Holster '{holsterName}' not added."));

                return new HolsterForItem(item, holster, holsterForItemView);
            });

            item.SetupHolsters(new HolstersForItem(holsters));

            if (storable)
                item.SetupStorable(_storedItems, 1);

            return item;
        }

        public ApparelPiece CreateApparel(string name, string slotName)
        {
            var slot = (ApparelSlot)_apparelSlots.All
                .FirstOrNone(x => x.Name == slotName)
                .Reduce(() => throw new TestSetupException($"Slot '{slotName}' not added."));

            return new ApparelPiece(name, slot, new ApparelViewFake());
        }

        public void VerifyViewCalls(params ViewCallInfo[] expectedViewCalls) =>
            _viewCallsTracker.VerifyViewCalls(expectedViewCalls);
    }
}