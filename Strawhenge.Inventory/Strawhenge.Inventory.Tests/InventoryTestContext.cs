using System;
using System.Collections.Generic;
using System.Linq;
using FunctionalUtilities;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Items.Holsters;
using Strawhenge.Inventory.TransientItems;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests
{
    class InventoryTestContext
    {
        readonly ViewCallsTracker _viewCallsTracker;
        readonly Hands _hands;
        readonly Holsters _holsters;
        readonly StoredItems _storedItems;
        readonly ApparelSlotsFake _apparelSlots;
        readonly ItemGeneratorFake _itemGenerator;

        public InventoryTestContext(ITestOutputHelper testOutputHelper)
        {
            var logger = new TestOutputLogger(testOutputHelper);
            _viewCallsTracker = new ViewCallsTracker(logger);

            _storedItems = new StoredItems(logger);
            _hands = new Hands();
            _holsters = new Holsters(logger);
            _apparelSlots = new ApparelSlotsFake();
            _itemGenerator = new ItemGeneratorFake();

            Inventory = new Inventory(
                _storedItems,
                _hands,
                _holsters,
                _apparelSlots);

            var equipped = new EquippedItems(_hands, _holsters);
            TransientItemLocator = new TransientItemLocator(
                equipped,
                _storedItems,
                _itemGenerator);
        }

        public Inventory Inventory { get; }

        public TransientItemLocator TransientItemLocator { get; }

        public void AddHolsters(IEnumerable<string> holsters)
        {
            foreach (var holster in holsters)
                AddHolster(holster);
        }

        public void AddHolster(string name) => _holsters.Add(name);

        public void AddApparelSlot(string name) => _apparelSlots.Add(name);

        public void SetStorageCapacity(int capacity) => _storedItems.SetWeightCapacity(capacity);

        public void SetGeneratedItem(string name, Item item) => _itemGenerator.Set(name, item);

        public Item CreateItem(string name, ItemSize? size = null, string[] holsterNames = null, bool storable = false)
        {
            size ??= ItemSize.OneHanded;
            holsterNames ??= Array.Empty<string>();

            var itemView = new ItemViewFake(name);
            _viewCallsTracker.Track(itemView);

            var item = new Item(name, _hands, itemView, size.Value);

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

        public Item CreateTransientItem(string name, ItemSize? size = null)
        {
            size ??= ItemSize.OneHanded;

            var itemView = new ItemViewFake(name);
            _viewCallsTracker.Track(itemView);

            return new Item(name, _hands, itemView, size.Value, isTransient: true);
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