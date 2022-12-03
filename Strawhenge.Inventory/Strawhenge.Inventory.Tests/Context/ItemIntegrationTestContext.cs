using Moq;
using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Items.HolsterForItem;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.Context
{
    public class ItemIntegrationTestContext
    {
        static int _itemCount;

        readonly Hands _hands = new Hands();
        readonly Holster _firstHolster = new Holster("First");
        readonly Holster _secondHolster = new Holster("Second");
        readonly Holster _thirdHolster = new Holster("Third");
        readonly ILogger _logger;

        public ItemIntegrationTestContext(ITestOutputHelper testOutputHelper) =>
            _logger = new TestOutputLogger(testOutputHelper);

        public ItemContext CreateOneHandedItem() => CreateItem(ItemSize.OneHanded);

        public ItemContext CreateTwoHandedItem() => CreateItem(ItemSize.TwoHanded);

        ItemContext CreateItem(ItemSize itemSize)
        {
            _itemCount++;

            var itemViewMock = new Mock<IItemView>(MockBehavior.Strict);
            var firstHolsterViewMock = new Mock<IHolsterForItemView>(MockBehavior.Strict);
            var secondHolsterViewMock = new Mock<IHolsterForItemView>(MockBehavior.Strict);
            var thirdHolsterViewMock = new Mock<IHolsterForItemView>(MockBehavior.Strict);

            var instance = new Item(
                $"Test Item {_itemCount}",
                _hands,
                itemViewMock.Object,
                itemSize,
                GetHolstersForItem)
            {
                ClearFromHandsPreference = ClearFromHandsPreference.PutAway
            };

            HolstersForItem GetHolstersForItem(IItem item)
            {
                var holstersForItem = new IHolsterForItem[]
                {
                    new HolsterForItem(item, _firstHolster, firstHolsterViewMock.Object),
                    new HolsterForItem(item, _secondHolster, secondHolsterViewMock.Object),
                    new HolsterForItem(item, _thirdHolster, thirdHolsterViewMock.Object)
                };

                return new HolstersForItem(holstersForItem, _logger);
            }

            return new ItemContext
            {
                Instance = instance,
                ItemViewMock = itemViewMock,
                FirstHolsterViewMock = firstHolsterViewMock,
                SecondHolsterViewMock = secondHolsterViewMock,
                ThirdHolsterViewMock = thirdHolsterViewMock
            };
        }
    }
}