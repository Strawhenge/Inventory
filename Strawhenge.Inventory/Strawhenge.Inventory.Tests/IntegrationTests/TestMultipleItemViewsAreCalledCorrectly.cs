using Moq;
using Strawhenge.Inventory.Tests.Context;
using System;
using System.Linq.Expressions;
using Xunit;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.IntegrationTests
{
    public class TestMultipleItemViewsAreCalledCorrectly
    {
        readonly ItemContext item1;
        readonly ItemContext item2;
        readonly ItemContext twoHandedItem1;
        readonly ItemContext twoHandedItem2;
        readonly MockSequence sequence;

        public TestMultipleItemViewsAreCalledCorrectly(ITestOutputHelper testOutputHelper)
        {
            var context = new ItemIntegrationTestContext(testOutputHelper);

            item1 = context.CreateOneHandedItem();
            item2 = context.CreateOneHandedItem();

            twoHandedItem1 = context.CreateTwoHandedItem();
            twoHandedItem2 = context.CreateTwoHandedItem();

            sequence = new MockSequence();
        }

        [Fact]
        public void DrawFirstItemThenDrawSecondItemInSameHand()
        {
            SetupInSequence(item1.ItemViewMock, x => x.DrawRightHand(It.IsAny<Action>()));
            SetupInSequence(item1.ItemViewMock, x => x.PutAwayRightHand(It.IsAny<Action>()));
            SetupInSequence(item2.ItemViewMock, x => x.DrawRightHand(It.IsAny<Action>()));

            item1.Instance.HoldRightHand();
            item2.Instance.HoldRightHand();

            item1.ItemViewMock.VerifyOnce(x => x.DrawRightHand(It.IsAny<Action>()));
            item1.ItemViewMock.VerifyOnce(x => x.PutAwayRightHand(It.IsAny<Action>()));
            item2.ItemViewMock.VerifyOnce(x => x.DrawRightHand(It.IsAny<Action>()));
        }

        [Fact]
        public void EquipFirstItemToHolsterThenSecondItemToSameHolster()
        {
            SetupInSequence(item1.SecondHolsterViewMock, x => x.Show(It.IsAny<Action>()));
            SetupInSequence(item1.SecondHolsterViewMock, x => x.Hide(It.IsAny<Action>()));
            SetupInSequence(item2.SecondHolsterViewMock, x => x.Show(It.IsAny<Action>()));

            item1.SecondHolster.Equip();
            item2.SecondHolster.Equip();

            item1.SecondHolsterViewMock.VerifyOnce(x => x.Show(It.IsAny<Action>()));
            item1.SecondHolsterViewMock.VerifyOnce(x => x.Hide(It.IsAny<Action>()));
            item2.SecondHolsterViewMock.VerifyOnce(x => x.Show(It.IsAny<Action>()));
        }

        [Fact]
        public void EquipFirstItemToHolsterDrawItemThenEquipSecondItemToHolsterThenPutAway()
        {
            SetupInSequence(item1.FirstHolsterViewMock, x => x.Show(It.IsAny<Action>()));
            SetupInSequence(item1.FirstHolsterViewMock, x => x.DrawLeftHand(It.IsAny<Action>()));
            SetupInSequence(item2.FirstHolsterViewMock, x => x.Show(It.IsAny<Action>()));
            SetupInSequence(item1.ItemViewMock, x => x.PutAwayLeftHand(It.IsAny<Action>()));

            item1.FirstHolster.Equip();
            item1.Instance.HoldLeftHand();
            item2.FirstHolster.Equip();
            item1.Instance.PutAway();

            item1.FirstHolsterViewMock.VerifyOnce(x => x.Show(It.IsAny<Action>()));
            item1.FirstHolsterViewMock.VerifyOnce(x => x.DrawLeftHand(It.IsAny<Action>()));
            item2.FirstHolsterViewMock.VerifyOnce(x => x.Show(It.IsAny<Action>()));
            item1.ItemViewMock.VerifyOnce(x => x.PutAwayLeftHand(It.IsAny<Action>()));
        }

        [Fact]
        public void HoldItemsInEachHandThenHoldTwoHandedItemThenHoldOneHandedItem()
        {
            SetupInSequence(item1.ItemViewMock, x => x.DrawLeftHand(It.IsAny<Action>()));
            SetupInSequence(item2.ItemViewMock, x => x.DrawRightHand(It.IsAny<Action>()));
            SetupInSequence(item2.ItemViewMock, x => x.PutAwayRightHand(It.IsAny<Action>()));
            SetupInSequence(item1.ItemViewMock, x => x.PutAwayLeftHand(It.IsAny<Action>()));
            SetupInSequence(twoHandedItem1.ItemViewMock, x => x.DrawRightHand(It.IsAny<Action>()));
            SetupInSequence(twoHandedItem1.ItemViewMock, x => x.PutAwayRightHand(It.IsAny<Action>()));
            SetupInSequence(item1.ItemViewMock, x => x.DrawLeftHand(It.IsAny<Action>()));

            item1.Instance.HoldLeftHand();
            item2.Instance.HoldRightHand();
            twoHandedItem1.Instance.HoldRightHand();
            item1.Instance.HoldLeftHand();

            item1.ItemViewMock.Verify(x => x.DrawLeftHand(It.IsAny<Action>()), Times.Exactly(2));
            item2.ItemViewMock.VerifyOnce(x => x.DrawRightHand(It.IsAny<Action>()));
            item2.ItemViewMock.VerifyOnce(x => x.PutAwayRightHand(It.IsAny<Action>()));
            item1.ItemViewMock.VerifyOnce(x => x.PutAwayLeftHand(It.IsAny<Action>()));
            twoHandedItem1.ItemViewMock.VerifyOnce(x => x.DrawRightHand(It.IsAny<Action>()));
            twoHandedItem1.ItemViewMock.VerifyOnce(x => x.PutAwayRightHand(It.IsAny<Action>()));
        }

        [Fact]
        public void HoldTwoHandedItemThenSwapHandThenHoldOtherTwoHandedItem()
        {
            SetupInSequence(twoHandedItem1.ItemViewMock, x => x.DrawRightHand(It.IsAny<Action>()));
            SetupInSequence(twoHandedItem1.ItemViewMock, x => x.RightHandToLeftHand(It.IsAny<Action>()));
            SetupInSequence(twoHandedItem1.ItemViewMock, x => x.PutAwayLeftHand(It.IsAny<Action>()));
            SetupInSequence(twoHandedItem2.ItemViewMock, x => x.DrawRightHand(It.IsAny<Action>()));

            twoHandedItem1.Instance.HoldRightHand();
            twoHandedItem1.Instance.HoldLeftHand();
            twoHandedItem2.Instance.HoldRightHand();

            twoHandedItem1.ItemViewMock.VerifyOnce(x => x.DrawRightHand(It.IsAny<Action>()));
            twoHandedItem1.ItemViewMock.VerifyOnce(x => x.RightHandToLeftHand(It.IsAny<Action>()));
            twoHandedItem1.ItemViewMock.VerifyOnce(x => x.PutAwayLeftHand(It.IsAny<Action>()));
            twoHandedItem2.ItemViewMock.VerifyOnce(x => x.DrawRightHand(It.IsAny<Action>()));
        }

        void SetupInSequence<T>(Mock<T> mock, Expression<Action<T>> expression) where T : class =>
            mock.InSequence(sequence).Setup(expression);
    }
}
