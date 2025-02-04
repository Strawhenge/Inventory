using System;
using System.Linq;
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

        public InventoryTestContext(ITestOutputHelper testOutputHelper)
        {
            var logger = new TestOutputLogger(testOutputHelper);
            _viewCallsTracker = new ViewCallsTracker(logger);

            _storedItems = new StoredItems();
            _hands = new Hands();
            _holsters = new Holsters(logger);

            Inventory = new Inventory(
                _storedItems,
                _hands,
                _holsters,
                new ApparelSlotsFake());
        }

        public Inventory Inventory { get; }

        public IStoredItemsWeightCapacitySetter StoredItemsWeightCapacity => _storedItems;

        public void AddHolster(string name) => _holsters.Add(name);

        public Item CreateItem(string name, ItemSize size, string[] holsterNames, bool storable)
        {
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

        public void VerifyViewCalls(params ViewCallInfo[] expectedViewCalls) =>
            _viewCallsTracker.VerifyViewCalls(expectedViewCalls);
    }
}