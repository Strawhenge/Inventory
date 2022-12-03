using Moq;
using System;
using Xunit;

namespace Strawhenge.Inventory.Tests.UnitTests
{
    public partial class Item_Tests
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void HoldLeftHand_ShouldDrawLeftHand_WhenNotInHand(bool shouldTestCallback)
        {
            HoldLeftHand(shouldTestCallback);

            _itemViewMock.VerifyOnce(
                x => x.DrawLeftHand(It.IsAny<Action>()));

            VerifyItemWasSetToLeftHand();
            VerifyNoOtherViewCalls();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void HoldLeftHand_ShouldDoNothing_WhenAlreadyInLeftHand(bool shouldTestCallback)
        {
            ArrangeItemInLeftHand();

            HoldLeftHand(shouldTestCallback);

            VerifyNoOtherViewCalls();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void HoldLeftHand_ShouldSwapFromRightHand_WhenItemInRightHand(bool shouldTestCallback)
        {
            ArrangeItemInRightHand();

            HoldLeftHand(shouldTestCallback);

            _itemViewMock.VerifyOnce(
               x => x.RightHandToLeftHand(It.IsAny<Action>()));

            VerifyItemWasSetToLeftHand();
            VerifyItemWasUnsetFromRightHand();
            VerifyNoOtherViewCalls();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void HoldLeftHand_ShouldDrawLeftHandFromHolster_WhenNotInHand_AndEquippedToHolster(bool shouldTestCallback)
        {
            var holsterViewMock = ArrangeEquippedToHolster();

            HoldLeftHand(shouldTestCallback);

            holsterViewMock.VerifyOnce(
                x => x.DrawLeftHand(It.IsAny<Action>()));

            holsterViewMock.VerifyNoOtherCalls();

            VerifyItemWasSetToLeftHand();
            VerifyNoOtherViewCalls();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void HoldLeftHand_ShouldClearItemInRightHand_WhenTwoHanded(bool shouldTestCallback)
        {
            ArrangeTwoHanded();
            var otherItemMock = ArrangeOtherItemInRightHand();

            HoldLeftHand(shouldTestCallback);

            otherItemMock.VerifyOnce(x => x.ClearFromHands(It.IsAny<Action>()));

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void HoldLeftHand_ShouldClearItemInRightHand_WhenOtherItemTwoHanded(bool shouldTestCallback)
        {
            var otherItemMock = ArrangeOtherItemInRightHand(isTwoHanded: true);

            HoldLeftHand(shouldTestCallback);

            otherItemMock.VerifyOnce(x => x.ClearFromHands(It.IsAny<Action>()));

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void HoldLeftHand_ShouldSwapFromRightHand_WhenItemInRightHand_AndIsTwoHanded(bool shouldTestCallback)
        {
            ArrangeTwoHandedItemInRightHand();

            HoldLeftHand(shouldTestCallback);

            _itemViewMock.VerifyOnce(x => x.RightHandToLeftHand(It.IsAny<Action>()));
            VerifyNoOtherViewCalls();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        void HoldLeftHand(bool shouldTestCallback)
        {
            if (shouldTestCallback)
                _item.HoldLeftHand(_callback);
            else
                _item.HoldLeftHand();
        }
    }
}
