using FunctionalUtilities;
using Moq;
using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Items.Storables;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Items.Consumables;
using Strawhenge.Inventory.Items.HolsterForItem;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.Context
{
    public class ItemIntegrationTestContext
    {
        static int _itemCount;

        readonly Hands _hands = new Hands();
        readonly ItemContainer _firstItemContainer = new ItemContainer("First");
        readonly ItemContainer _secondItemContainer = new ItemContainer("Second");
        readonly ItemContainer _thirdItemContainer = new ItemContainer("Third");
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
                GetHolstersForItem,
                _ => Maybe.None<IConsumable>(),
                _ => Maybe.None<IStorable>())
            {
                ClearFromHandsPreference = ClearFromHandsPreference.PutAway
            };

            HolstersForItem GetHolstersForItem(IItem item)
            {
                var holstersForItem = new IHolsterForItem[]
                {
                    new HolsterForItem(item, _firstItemContainer, firstHolsterViewMock.Object),
                    new HolsterForItem(item, _secondItemContainer, secondHolsterViewMock.Object),
                    new HolsterForItem(item, _thirdItemContainer, thirdHolsterViewMock.Object)
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