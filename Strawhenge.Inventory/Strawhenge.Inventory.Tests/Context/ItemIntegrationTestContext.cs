﻿using Moq;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Items.HolsterForItem;

namespace Strawhenge.Inventory.Tests.Context
{
    public class ItemIntegrationTestContext
    {
        static int _itemCount;

        readonly Hands _hands = new Hands();
        readonly ItemContainer _firstItemContainer = new ItemContainer("First");
        readonly ItemContainer _secondItemContainer = new ItemContainer("Second");
        readonly ItemContainer _thirdItemContainer = new ItemContainer("Third");

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
                itemSize)
            {
                ClearFromHandsPreference = ClearFromHandsPreference.PutAway
            };

            instance.SetupHolsters(new (ItemContainer, IHolsterForItemView)[]
            {
                (_firstItemContainer, firstHolsterViewMock.Object),
                (_secondItemContainer, secondHolsterViewMock.Object),
                (_thirdItemContainer, thirdHolsterViewMock.Object),
            });

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