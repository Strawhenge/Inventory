using Moq;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Items.HolsterForItem;
using Strawhenge.Inventory.Tests.Context;
using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Strawhenge.Inventory.Tests.IntegrationTests
{
    public class TestItemViewsAreCalledCorrectly
    {
        readonly IItem _item;
        readonly IEquipItemToHolster _firstHolster;
        readonly IEquipItemToHolster _secondHolster;
        readonly IEquipItemToHolster _thirdHolster;
        readonly MockSequence _sequence;
        readonly Mock<IItemView> _itemViewMock;
        readonly Mock<IHolsterForItemView> _firstHolsterViewMock;
        readonly Mock<IHolsterForItemView> _secondHolsterViewMock;
        readonly Mock<IHolsterForItemView> _thirdHolsterViewMock;

        public TestItemViewsAreCalledCorrectly()
        {
            _sequence = new MockSequence();

            var context = new ItemIntegrationTestContext();
            var itemContext = context.CreateOneHandedItem();

            _item = itemContext.Instance;
            _itemViewMock = itemContext.ItemViewMock;
            _firstHolsterViewMock = itemContext.FirstHolsterViewMock;
            _secondHolsterViewMock = itemContext.SecondHolsterViewMock;
            _thirdHolsterViewMock = itemContext.ThirdHolsterViewMock;

            _firstHolster = _item.Holsters.ElementAt(0);
            _secondHolster = _item.Holsters.ElementAt(1);
            _thirdHolster = _item.Holsters.ElementAt(2);
        }

        [Fact]
        public void DrawRightHandSwapToLeftThenPutAway()
        {
            SetupInSequence(_itemViewMock, x => x.DrawRightHand(It.IsAny<Action>()));
            SetupInSequence(_itemViewMock, x => x.RightHandToLeftHand(It.IsAny<Action>()));
            SetupInSequence(_itemViewMock, x => x.PutAwayLeftHand(It.IsAny<Action>()));

            _item.HoldRightHand();
            _item.HoldLeftHand();
            _item.PutAway();

            _itemViewMock.VerifyOnce(x => x.DrawRightHand(It.IsAny<Action>()));
            _itemViewMock.VerifyOnce(x => x.RightHandToLeftHand(It.IsAny<Action>()));
            _itemViewMock.VerifyOnce(x => x.PutAwayLeftHand(It.IsAny<Action>()));
        }

        [Fact]
        public void EquipHolsterDrawLeftHandPutAwayDrawRightUnequipPutAway()
        {
            SetupInSequence(_firstHolsterViewMock, x => x.Show(It.IsAny<Action>()));
            SetupInSequence(_firstHolsterViewMock, x => x.DrawLeftHand(It.IsAny<Action>()));
            SetupInSequence(_firstHolsterViewMock, x => x.PutAwayLeftHand(It.IsAny<Action>()));
            SetupInSequence(_firstHolsterViewMock, x => x.DrawRightHand(It.IsAny<Action>()));
            SetupInSequence(_itemViewMock, x => x.PutAwayRightHand(It.IsAny<Action>()));

            _firstHolster.Equip();
            _item.HoldLeftHand();
            _item.PutAway();
            _item.HoldRightHand();
            _firstHolster.Unequip();
            _item.PutAway();

            _firstHolsterViewMock.VerifyOnce(x => x.Show(It.IsAny<Action>()));
            _firstHolsterViewMock.VerifyOnce(x => x.DrawLeftHand(It.IsAny<Action>()));
            _firstHolsterViewMock.VerifyOnce(x => x.PutAwayLeftHand(It.IsAny<Action>()));
            _firstHolsterViewMock.VerifyOnce(x => x.DrawRightHand(It.IsAny<Action>()));
            _itemViewMock.VerifyOnce(x => x.PutAwayRightHand(It.IsAny<Action>()));
        }

        [Fact]
        public void EquipHolsterThenDrawThenChangeHolsterPutAwayThenChangeHolster()
        {
            SetupInSequence(_firstHolsterViewMock, x => x.Show(It.IsAny<Action>()));
            SetupInSequence(_firstHolsterViewMock, x => x.DrawRightHand(It.IsAny<Action>()));
            SetupInSequence(_secondHolsterViewMock, x => x.PutAwayRightHand(It.IsAny<Action>()));
            SetupInSequence(_secondHolsterViewMock, x => x.Hide(It.IsAny<Action>()));
            SetupInSequence(_thirdHolsterViewMock, x => x.Show(It.IsAny<Action>()));

            _firstHolster.Equip();
            _item.HoldRightHand();
            _secondHolster.Equip();
            _item.PutAway();
            _thirdHolster.Equip();

            _firstHolsterViewMock.VerifyOnce(x => x.Show(It.IsAny<Action>()));
            _firstHolsterViewMock.VerifyOnce(x => x.DrawRightHand(It.IsAny<Action>()));
            _secondHolsterViewMock.VerifyOnce(x => x.PutAwayRightHand(It.IsAny<Action>()));
            _secondHolsterViewMock.VerifyOnce(x => x.Hide(It.IsAny<Action>()));
            _thirdHolsterViewMock.VerifyOnce(x => x.Show(It.IsAny<Action>()));
        }

        void SetupInSequence<T>(Mock<T> mock, Expression<Action<T>> expression) where T : class =>
            mock.InSequence(_sequence).Setup(expression);
    }
}