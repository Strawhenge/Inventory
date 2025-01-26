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

        public InventoryTestContext(ITestOutputHelper testOutputHelper)
        {
            var logger = new TestOutputLogger(testOutputHelper);
            _viewCallsTracker = new ViewCallsTracker(logger);

            _hands = new Hands();
            _holsters = new Holsters(logger);

            Inventory = new Inventory(
                new StoredItems(),
                _hands,
                _holsters,
                new ApparelSlotsFake());
        }

        public Inventory Inventory { get; }

        public void AddHolster(string name) => _holsters.Add(name);

        public Item CreateItem(string name) => CreateItem(name, Array.Empty<string>());

        public Item CreateItem(string name, string[] holsterNames)
        {
            var itemView = new ItemViewFake(name);
            _viewCallsTracker.Track(itemView);

            var item = new Item(name, _hands, itemView, ItemSize.OneHanded);

            var holsters = holsterNames.Select(holsterName =>
            {
                var holsterForItemView = new HolsterForItemViewFake(name, holsterName);
                _viewCallsTracker.Track(holsterForItemView);

                return new HolsterForItem(item, (ItemContainer)_holsters.FindByName(holsterName), holsterForItemView);
            });

            item.SetupHolsters(new HolstersForItem(holsters));

            return item;
        }

        public void VerifyViewCalls(params ViewCallInfo[] expectedViewCalls) =>
            _viewCallsTracker.VerifyViewCalls(expectedViewCalls);
    }
}