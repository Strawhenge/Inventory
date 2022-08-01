using Moq;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Items.HolsterForItem;
using Strawhenge.Inventory.Tests.Context;
using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.IntegrationTests
{
    public class TestItemViewsAreCalledCorrectly
    {
        readonly IItem item;
        readonly IHolsterForItem firstHolster;
        readonly IHolsterForItem secondHolster;
        readonly IHolsterForItem thirdHolster;
        readonly MockSequence sequence;
        readonly Mock<IItemView> itemViewMock;
        readonly Mock<IHolsterForItemView> firstHolsterViewMock;
        readonly Mock<IHolsterForItemView> secondHolsterViewMock;
        readonly Mock<IHolsterForItemView> thirdHolsterViewMock;

        public TestItemViewsAreCalledCorrectly(ITestOutputHelper testOutputHelper)
        {
            sequence = new MockSequence();

            var context = new ItemIntegrationTestContext(testOutputHelper);
            var itemContext = context.CreateOneHandedItem();

            item = itemContext.Instance;
            itemViewMock = itemContext.ItemViewMock;
            firstHolsterViewMock = itemContext.FirstHolsterViewMock;
            secondHolsterViewMock = itemContext.SecondHolsterViewMock;
            thirdHolsterViewMock = itemContext.ThirdHolsterViewMock;

            firstHolster = item.Holsters.ElementAt(0);
            secondHolster = item.Holsters.ElementAt(1);
            thirdHolster = item.Holsters.ElementAt(2);
        }

        [Fact]
        public void DrawRightHandSwapToLeftThenPutAway()
        {
            SetupInSequence(itemViewMock, x => x.DrawRightHand(It.IsAny<Action>()));
            SetupInSequence(itemViewMock, x => x.RightHandToLeftHand(It.IsAny<Action>()));
            SetupInSequence(itemViewMock, x => x.PutAwayLeftHand(It.IsAny<Action>()));

            item.HoldRightHand();
            item.HoldLeftHand();
            item.PutAway();

            itemViewMock.VerifyOnce(x => x.DrawRightHand(It.IsAny<Action>()));
            itemViewMock.VerifyOnce(x => x.RightHandToLeftHand(It.IsAny<Action>()));
            itemViewMock.VerifyOnce(x => x.PutAwayLeftHand(It.IsAny<Action>()));
        }

        [Fact]
        public void EquipHolsterDrawLeftHandPutAwayDrawRightUnequipPutAway()
        {
            SetupInSequence(firstHolsterViewMock, x => x.Show(It.IsAny<Action>()));
            SetupInSequence(firstHolsterViewMock, x => x.DrawLeftHand(It.IsAny<Action>()));
            SetupInSequence(firstHolsterViewMock, x => x.PutAwayLeftHand(It.IsAny<Action>()));
            SetupInSequence(firstHolsterViewMock, x => x.DrawRightHand(It.IsAny<Action>()));
            SetupInSequence(itemViewMock, x => x.PutAwayRightHand(It.IsAny<Action>()));

            firstHolster.Equip();
            item.HoldLeftHand();
            item.PutAway();
            item.HoldRightHand();
            firstHolster.Unequip();
            item.PutAway();

            firstHolsterViewMock.VerifyOnce(x => x.Show(It.IsAny<Action>()));
            firstHolsterViewMock.VerifyOnce(x => x.DrawLeftHand(It.IsAny<Action>()));
            firstHolsterViewMock.VerifyOnce(x => x.PutAwayLeftHand(It.IsAny<Action>()));
            firstHolsterViewMock.VerifyOnce(x => x.DrawRightHand(It.IsAny<Action>()));
            itemViewMock.VerifyOnce(x => x.PutAwayRightHand(It.IsAny<Action>()));
        }

        [Fact]
        public void EquipHolsterThenDrawThenChangeHolsterPutAwayThenChangeHolster()
        {
            SetupInSequence(firstHolsterViewMock, x => x.Show(It.IsAny<Action>()));
            SetupInSequence(firstHolsterViewMock, x => x.DrawRightHand(It.IsAny<Action>()));
            SetupInSequence(secondHolsterViewMock, x => x.PutAwayRightHand(It.IsAny<Action>()));
            SetupInSequence(secondHolsterViewMock, x => x.Hide(It.IsAny<Action>()));
            SetupInSequence(thirdHolsterViewMock, x => x.Show(It.IsAny<Action>()));

            firstHolster.Equip();
            item.HoldRightHand();
            secondHolster.Equip();
            item.PutAway();
            thirdHolster.Equip();

            firstHolsterViewMock.VerifyOnce(x => x.Show(It.IsAny<Action>()));
            firstHolsterViewMock.VerifyOnce(x => x.DrawRightHand(It.IsAny<Action>()));
            secondHolsterViewMock.VerifyOnce(x => x.PutAwayRightHand(It.IsAny<Action>()));
            secondHolsterViewMock.VerifyOnce(x => x.Hide(It.IsAny<Action>()));
            thirdHolsterViewMock.VerifyOnce(x => x.Show(It.IsAny<Action>()));
        }

        void SetupInSequence<T>(Mock<T> mock, Expression<Action<T>> expression) where T : class =>
            mock.InSequence(sequence).Setup(expression);
    }
}
