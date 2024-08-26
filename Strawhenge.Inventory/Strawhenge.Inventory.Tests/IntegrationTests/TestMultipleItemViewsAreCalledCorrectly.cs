using Moq;
using Strawhenge.Inventory.Tests.Context;
using System;
using System.Linq.Expressions;
using Xunit;

namespace Strawhenge.Inventory.Tests.IntegrationTests
{
    public class TestMultipleItemViewsAreCalledCorrectly
    {
        readonly ItemContext _item1;
        readonly ItemContext _item2;
        readonly ItemContext _twoHandedItem1;
        readonly ItemContext _twoHandedItem2;
        readonly MockSequence _sequence;

        public TestMultipleItemViewsAreCalledCorrectly()
        {
            var context = new ItemIntegrationTestContext();

            _item1 = context.CreateOneHandedItem();
            _item2 = context.CreateOneHandedItem();

            _twoHandedItem1 = context.CreateTwoHandedItem();
            _twoHandedItem2 = context.CreateTwoHandedItem();

            _sequence = new MockSequence();
        }

        [Fact]
        public void DrawFirstItemThenDrawSecondItemInSameHand()
        {
            SetupInSequence(_item1.ItemViewMock, x => x.DrawRightHand(It.IsAny<Action>()));
            SetupInSequence(_item1.ItemViewMock, x => x.PutAwayRightHand(It.IsAny<Action>()));
            SetupInSequence(_item2.ItemViewMock, x => x.DrawRightHand(It.IsAny<Action>()));

            _item1.Instance.HoldRightHand();
            _item2.Instance.HoldRightHand();

            _item1.ItemViewMock.VerifyOnce(x => x.DrawRightHand(It.IsAny<Action>()));
            _item1.ItemViewMock.VerifyOnce(x => x.PutAwayRightHand(It.IsAny<Action>()));
            _item2.ItemViewMock.VerifyOnce(x => x.DrawRightHand(It.IsAny<Action>()));
        }

        [Fact]
        public void EquipFirstItemToHolsterThenSecondItemToSameHolster()
        {
            SetupInSequence(_item1.SecondHolsterViewMock, x => x.Show(It.IsAny<Action>()));
            SetupInSequence(_item1.SecondHolsterViewMock, x => x.Hide(It.IsAny<Action>()));
            SetupInSequence(_item2.SecondHolsterViewMock, x => x.Show(It.IsAny<Action>()));

            _item1.SecondHolster.Equip();
            _item2.SecondHolster.Equip();

            _item1.SecondHolsterViewMock.VerifyOnce(x => x.Show(It.IsAny<Action>()));
            _item1.SecondHolsterViewMock.VerifyOnce(x => x.Hide(It.IsAny<Action>()));
            _item2.SecondHolsterViewMock.VerifyOnce(x => x.Show(It.IsAny<Action>()));
        }

        [Fact]
        public void EquipFirstItemToHolsterDrawItemThenEquipSecondItemToHolsterThenPutAway()
        {
            SetupInSequence(_item1.FirstHolsterViewMock, x => x.Show(It.IsAny<Action>()));
            SetupInSequence(_item1.FirstHolsterViewMock, x => x.DrawLeftHand(It.IsAny<Action>()));
            SetupInSequence(_item2.FirstHolsterViewMock, x => x.Show(It.IsAny<Action>()));
            SetupInSequence(_item1.ItemViewMock, x => x.PutAwayLeftHand(It.IsAny<Action>()));

            _item1.FirstHolster.Equip();
            _item1.Instance.HoldLeftHand();
            _item2.FirstHolster.Equip();
            _item1.Instance.PutAway();

            _item1.FirstHolsterViewMock.VerifyOnce(x => x.Show(It.IsAny<Action>()));
            _item1.FirstHolsterViewMock.VerifyOnce(x => x.DrawLeftHand(It.IsAny<Action>()));
            _item2.FirstHolsterViewMock.VerifyOnce(x => x.Show(It.IsAny<Action>()));
            _item1.ItemViewMock.VerifyOnce(x => x.PutAwayLeftHand(It.IsAny<Action>()));
        }

        [Fact]
        public void HoldItemsInEachHandThenHoldTwoHandedItemThenHoldOneHandedItem()
        {
            SetupInSequence(_item1.ItemViewMock, x => x.DrawLeftHand(It.IsAny<Action>()));
            SetupInSequence(_item2.ItemViewMock, x => x.DrawRightHand(It.IsAny<Action>()));
            SetupInSequence(_item2.ItemViewMock, x => x.PutAwayRightHand(It.IsAny<Action>()));
            SetupInSequence(_item1.ItemViewMock, x => x.PutAwayLeftHand(It.IsAny<Action>()));
            SetupInSequence(_twoHandedItem1.ItemViewMock, x => x.DrawRightHand(It.IsAny<Action>()));
            SetupInSequence(_twoHandedItem1.ItemViewMock, x => x.PutAwayRightHand(It.IsAny<Action>()));
            SetupInSequence(_item1.ItemViewMock, x => x.DrawLeftHand(It.IsAny<Action>()));

            _item1.Instance.HoldLeftHand();
            _item2.Instance.HoldRightHand();
            _twoHandedItem1.Instance.HoldRightHand();
            _item1.Instance.HoldLeftHand();

            _item1.ItemViewMock.Verify(x => x.DrawLeftHand(It.IsAny<Action>()), Times.Exactly(2));
            _item2.ItemViewMock.VerifyOnce(x => x.DrawRightHand(It.IsAny<Action>()));
            _item2.ItemViewMock.VerifyOnce(x => x.PutAwayRightHand(It.IsAny<Action>()));
            _item1.ItemViewMock.VerifyOnce(x => x.PutAwayLeftHand(It.IsAny<Action>()));
            _twoHandedItem1.ItemViewMock.VerifyOnce(x => x.DrawRightHand(It.IsAny<Action>()));
            _twoHandedItem1.ItemViewMock.VerifyOnce(x => x.PutAwayRightHand(It.IsAny<Action>()));
        }

        [Fact]
        public void HoldTwoHandedItemThenSwapHandThenHoldOtherTwoHandedItem()
        {
            SetupInSequence(_twoHandedItem1.ItemViewMock, x => x.DrawRightHand(It.IsAny<Action>()));
            SetupInSequence(_twoHandedItem1.ItemViewMock, x => x.RightHandToLeftHand(It.IsAny<Action>()));
            SetupInSequence(_twoHandedItem1.ItemViewMock, x => x.PutAwayLeftHand(It.IsAny<Action>()));
            SetupInSequence(_twoHandedItem2.ItemViewMock, x => x.DrawRightHand(It.IsAny<Action>()));

            _twoHandedItem1.Instance.HoldRightHand();
            _twoHandedItem1.Instance.HoldLeftHand();
            _twoHandedItem2.Instance.HoldRightHand();

            _twoHandedItem1.ItemViewMock.VerifyOnce(x => x.DrawRightHand(It.IsAny<Action>()));
            _twoHandedItem1.ItemViewMock.VerifyOnce(x => x.RightHandToLeftHand(It.IsAny<Action>()));
            _twoHandedItem1.ItemViewMock.VerifyOnce(x => x.PutAwayLeftHand(It.IsAny<Action>()));
            _twoHandedItem2.ItemViewMock.VerifyOnce(x => x.DrawRightHand(It.IsAny<Action>()));
        }

        void SetupInSequence<T>(Mock<T> mock, Expression<Action<T>> expression) where T : class =>
            mock.InSequence(_sequence).Setup(expression);
    }
}