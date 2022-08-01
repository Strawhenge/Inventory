using Moq;
using Strawhenge.Inventory.Containers;
using Xunit;

namespace Strawhenge.Inventory.Tests.UnitTests
{
    public class Hands_Tests
    {
        readonly Hands hands;
        readonly Mock<IItem> itemMock;
        readonly IItem item;

        public Hands_Tests()
        {
            hands = new Hands();

            itemMock = new Mock<IItem>();
            item = itemMock.Object;
        }

        [Fact]
        public void Init()
        {
            AssertMaybe.IsNone(hands.ItemInLeftHand);
            AssertMaybe.IsNone(hands.ItemInRightHand);
            AssertDoesNotHaveTwoHandedItem();
            AssertDoesNotHaveItemInLeftHand();
            AssertDoesNotHaveItemInRightHand();
        }

        [Fact]
        public void SetItemLeftHand()
        {
            hands.SetItemLeftHand(item);

            AssertMaybe.IsSome(hands.ItemInLeftHand, item);
            AssertMaybe.IsNone(hands.ItemInRightHand);
            AssertDoesNotHaveItemInRightHand();
            AssertDoesNotHaveTwoHandedItem();
        }

        [Fact]
        public void SetItemRightHand()
        {
            hands.SetItemRightHand(item);

            AssertMaybe.IsSome(hands.ItemInRightHand, item);
            AssertMaybe.IsNone(hands.ItemInLeftHand);
            AssertDoesNotHaveItemInLeftHand();
            AssertDoesNotHaveTwoHandedItem();
        }

        [Fact]
        public void SetItemLeftHand_WhenItemIsTwoHanded()
        {
            ArrangeItemIsTwoHanded();

            hands.SetItemLeftHand(item);

            AssertMaybe.IsSome(hands.ItemInLeftHand, item);
            AssertMaybe.IsNone(hands.ItemInRightHand);
            AssertDoesNotHaveItemInRightHand();
            AssertDoesHaveTwoHandedItem();
        }

        [Fact]
        public void SetItemRightHand_WhenItemIsTwoHanded()
        {
            ArrangeItemIsTwoHanded();

            hands.SetItemRightHand(item);

            AssertMaybe.IsSome(hands.ItemInRightHand, item);
            AssertMaybe.IsNone(hands.ItemInLeftHand);
            AssertDoesNotHaveItemInLeftHand();
            AssertDoesHaveTwoHandedItem();
        }

        void ArrangeItemIsTwoHanded() => itemMock.SetupGet(x => x.IsTwoHanded).Returns(true);

        void AssertDoesNotHaveItemInLeftHand() => Assert.False(hands.IsInLeftHand(item));

        void AssertDoesNotHaveItemInRightHand() => Assert.False(hands.IsInRightHand(item));

        void AssertDoesNotHaveTwoHandedItem()
        {
            Assert.False(hands.HasTwoHandedItem(out var item));
            Assert.Null(item);
        }

        void AssertDoesHaveTwoHandedItem()
        {
            Assert.True(hands.HasTwoHandedItem(out var item));
            Assert.Same(this.item, item);
        }
    }
}
