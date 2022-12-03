using Moq;
using Strawhenge.Inventory.Containers;
using Xunit;

namespace Strawhenge.Inventory.Tests.UnitTests
{
    public class Hands_Tests
    {
        readonly Hands _hands;
        readonly Mock<IItem> _itemMock;
        readonly IItem _item;

        public Hands_Tests()
        {
            _hands = new Hands();

            _itemMock = new Mock<IItem>();
            _item = _itemMock.Object;
        }

        [Fact]
        public void Init()
        {
            AssertMaybe.IsNone(_hands.ItemInLeftHand);
            AssertMaybe.IsNone(_hands.ItemInRightHand);
            AssertDoesNotHaveTwoHandedItem();
            AssertDoesNotHaveItemInLeftHand();
            AssertDoesNotHaveItemInRightHand();
        }

        [Fact]
        public void SetItemLeftHand()
        {
            _hands.SetItemLeftHand(_item);

            AssertMaybe.IsSome(_hands.ItemInLeftHand, _item);
            AssertMaybe.IsNone(_hands.ItemInRightHand);
            AssertDoesNotHaveItemInRightHand();
            AssertDoesNotHaveTwoHandedItem();
        }

        [Fact]
        public void SetItemRightHand()
        {
            _hands.SetItemRightHand(_item);

            AssertMaybe.IsSome(_hands.ItemInRightHand, _item);
            AssertMaybe.IsNone(_hands.ItemInLeftHand);
            AssertDoesNotHaveItemInLeftHand();
            AssertDoesNotHaveTwoHandedItem();
        }

        [Fact]
        public void SetItemLeftHand_WhenItemIsTwoHanded()
        {
            ArrangeItemIsTwoHanded();

            _hands.SetItemLeftHand(_item);

            AssertMaybe.IsSome(_hands.ItemInLeftHand, _item);
            AssertMaybe.IsNone(_hands.ItemInRightHand);
            AssertDoesNotHaveItemInRightHand();
            AssertDoesHaveTwoHandedItem();
        }

        [Fact]
        public void SetItemRightHand_WhenItemIsTwoHanded()
        {
            ArrangeItemIsTwoHanded();

            _hands.SetItemRightHand(_item);

            AssertMaybe.IsSome(_hands.ItemInRightHand, _item);
            AssertMaybe.IsNone(_hands.ItemInLeftHand);
            AssertDoesNotHaveItemInLeftHand();
            AssertDoesHaveTwoHandedItem();
        }

        void ArrangeItemIsTwoHanded() => _itemMock.SetupGet(x => x.IsTwoHanded).Returns(true);

        void AssertDoesNotHaveItemInLeftHand() => Assert.False(_hands.IsInLeftHand(_item));

        void AssertDoesNotHaveItemInRightHand() => Assert.False(_hands.IsInRightHand(_item));

        void AssertDoesNotHaveTwoHandedItem()
        {
            Assert.False(_hands.HasTwoHandedItem(out var item));
            Assert.Null(item);
        }

        void AssertDoesHaveTwoHandedItem()
        {
            Assert.True(_hands.HasTwoHandedItem(out var item));
            Assert.Same(this._item, item);
        }
    }
}
