using Moq;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Items.HolsterForItem;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.Context
{
    public class ItemIntegrationTestContext
    {
        static int itemCount = 0;

        readonly Hands hands = new Hands();
        readonly Holster firstHolster = new Holster("First");
        readonly Holster secondHolster = new Holster("Second");
        readonly Holster thirdHolster = new Holster("Third");
        readonly ILogger logger;

        public ItemIntegrationTestContext(ITestOutputHelper testOutputHelper) => logger = new TestOutputLogger(testOutputHelper);

        public ItemContext CreateOneHandedItem() => CreateItem(ItemSize.OneHanded);

        public ItemContext CreateTwoHandedItem() => CreateItem(ItemSize.TwoHanded);

        private ItemContext CreateItem(ItemSize itemSize)
        {
            itemCount++;

            var itemViewMock = new Mock<IItemView>(MockBehavior.Strict);
            var firstHolsterViewMock = new Mock<IHolsterForItemView>(MockBehavior.Strict);
            var secondHolsterViewMock = new Mock<IHolsterForItemView>(MockBehavior.Strict);
            var thirdHolsterViewMock = new Mock<IHolsterForItemView>(MockBehavior.Strict);

            var instance = new Item(
                $"Test Item {itemCount}",
                hands,
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
                    new HolsterForItem(item, firstHolster, firstHolsterViewMock.Object),
                    new HolsterForItem(item, secondHolster, secondHolsterViewMock.Object),
                    new HolsterForItem(item, thirdHolster, thirdHolsterViewMock.Object)
                };

                return new HolstersForItem(holstersForItem, logger);
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
