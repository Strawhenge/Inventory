using System;
using System.Linq;
using FunctionalUtilities;
using Moq;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Items.Consumables;
using Strawhenge.Inventory.Items.HolsterForItem;
using Strawhenge.Inventory.Items.Storables;
using Xunit;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.UnitTests
{
    public class ItemDiscardTests
    {
        readonly StoredItems _storedItems;
        readonly IHands _hands;
        readonly ItemContainer _holsterContainer;
        readonly Mock<IItemView> _viewMock;
        readonly Mock<IHolsterForItemView> _holsterView;
        readonly Item _item;

        public ItemDiscardTests(ITestOutputHelper testOutputHelper)
        {
            var logger = new TestOutputLogger(testOutputHelper);

            _storedItems = new StoredItems();
            _hands = new Hands();
            _holsterContainer = new ItemContainer("Discard Test Item Holster");
            _viewMock = new Mock<IItemView>();
            _holsterView = new Mock<IHolsterForItemView>();

            _item = new Item("Discard Test Item", _hands, _viewMock.Object, ItemSize.OneHanded, item =>
            {
                var holster = new HolsterForItem(
                    item,
                    _holsterContainer,
                    _holsterView.Object);

                return new HolstersForItem(new[] { holster }, logger);
            });

            _item.SetupStorable(_storedItems, weight: 0);
            _item.Storable.Do(x => x.AddToStorage());
        }

        [Fact]
        public void Item_should_not_exist_in_stored_items_when_discarded_from_initial_state()
        {
            _item.Discard();

            VerifyItemNotInStoredItems();
        }

        [Theory, InlineData(true), InlineData(false)]
        public void Item_should_not_exist_in_hands_when_discarded_from_in_hand(bool right)
        {
            ArrangeHoldingItem(right);

            _item.Discard();

            VerifyItemNotInHands();
        }

        [Theory, InlineData(true), InlineData(false)]
        public void Item_should_not_exist_in_stored_items_when_discarded_from_in_hand(bool right)
        {
            ArrangeHoldingItem(right);

            _item.Discard();

            VerifyItemNotInStoredItems();
        }

        [Theory, InlineData(true), InlineData(false)]
        public void Item_should_disappear_from_view_when_discarded_from_in_hand(bool right)
        {
            ArrangeHoldingItem(right);

            _item.Discard();

            VerifyItemDisappearedFromView();
        }

        [Fact]
        public void Item_should_not_exist_in_holsters_when_discarded_from_holster()
        {
            ArrangeInHolster();

            _item.Discard();

            VerifyItemNotInHolster();
        }

        [Theory, InlineData(true), InlineData(false)]
        public void Item_should_not_exist_in_holsters_when_equipped_to_holster_and_discarded_from_in_hand(bool right)
        {
            ArrangeInHolster();
            ArrangeHoldingItem(right);

            _item.Discard();

            VerifyItemNotInHolster();
        }

        [Theory, InlineData(true), InlineData(false)]
        public void Item_should_disappear_from_view_when_equipped_to_holster_and_discarded_from_in_hand(bool right)
        {
            ArrangeInHolster();
            ArrangeHoldingItem(right);

            _item.Discard();

            VerifyItemDisappearedFromView();
        }

        [Fact]
        public void Item_should_disappear_from_holster_view_when_discarded_from_holster()
        {
            ArrangeInHolster();

            _item.Discard();

            VerifyItemDisappearedFromHolsterView();
        }

        void ArrangeInHolster() => _item.Holsters.First().Equip();

        void ArrangeHoldingItem(bool right)
        {
            if (right)
                _item.HoldRightHand();
            else
                _item.HoldLeftHand();
        }

        void VerifyItemNotInHands()
        {
            Assert.False(_item.IsInHand);
            AssertMaybe.IsNone(_hands.ItemInRightHand);
            AssertMaybe.IsNone(_hands.ItemInLeftHand);
        }

        void VerifyItemNotInHolster()
        {
            Assert.False(_item.Holsters.First().IsEquipped);
            AssertMaybe.IsNone(_holsterContainer.CurrentItem);
        }

        void VerifyItemNotInStoredItems() => Assert.DoesNotContain(_item, _storedItems.Items);

        void VerifyItemDisappearedFromView() => _viewMock.VerifyOnce(
            x => x.Disappear(It.IsAny<Action>()));

        void VerifyItemDisappearedFromHolsterView() => _holsterView.VerifyOnce(
            x => x.Hide(It.IsAny<Action>()));
    }
}