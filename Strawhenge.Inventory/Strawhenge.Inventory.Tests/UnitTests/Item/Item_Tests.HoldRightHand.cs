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
        public void HoldRightHand_ShouldDrawRightHand_WhenNotInHand(bool shouldTestCallback)
        {
            HoldRightHand(shouldTestCallback);

            itemViewMock.VerifyOnce(
                x => x.DrawRightHand(It.IsAny<Action>()));

            VerifyItemWasSetToRightHand();
            VerifyNoOtherViewCalls();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void HoldRightHand_ShouldDoNothing_WhenAlreadyInRightHand(bool shouldTestCallback)
        {
            ArrangeItemInRightHand();

            HoldRightHand(shouldTestCallback);

            VerifyNoOtherViewCalls();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void HoldRightHand_ShouldSwapFromLeftHand_WhenItemInLeftHand(bool shouldTestCallback)
        {
            ArrangeItemInLeftHand();

            HoldRightHand(shouldTestCallback);

            itemViewMock.VerifyOnce(
               x => x.LeftHandToRightHand(It.IsAny<Action>()));

            VerifyItemWasSetToRightHand();
            VerifyItemWasUnsetFromLeftHand();
            VerifyNoOtherViewCalls();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void HoldRightHand_ShouldDrawRightHandFromHolster_WhenNotInHand_AndEquippedToHolster(bool shouldTestCallback)
        {
            var holsterViewMock = ArrangeEquippedToHolster();

            HoldRightHand(shouldTestCallback);

            holsterViewMock.VerifyOnce(
                x => x.DrawRightHand(It.IsAny<Action>()));

            holsterViewMock.VerifyNoOtherCalls();

            VerifyItemWasSetToRightHand();
            VerifyNoOtherViewCalls();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void HoldRightHand_ShouldClearItemInLeftHand_WhenTwoHanded(bool shouldTestCallback)
        {
            ArrangeTwoHanded();
            var otherItemMock = ArrangeOtherItemInLeftHand();

            HoldRightHand(shouldTestCallback);

            otherItemMock.VerifyOnce(x => x.ClearFromHands(It.IsAny<Action>()));

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void HoldRightHand_ShouldClearItemInLeftHand_WhenOtherItemTwoHanded(bool shouldTestCallback)
        {
            var otherItemMock = ArrangeOtherItemInLeftHand(isTwoHanded: true);

            HoldRightHand(shouldTestCallback);

            otherItemMock.VerifyOnce(x => x.ClearFromHands(It.IsAny<Action>()));

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void HoldRightHand_ShouldSwapFromLeftHand_WhenItemInLeftHand_AndIsTwoHanded(bool shouldTestCallback)
        {
            ArrangeTwoHandedItemInLeftHand();

            HoldRightHand(shouldTestCallback);

            itemViewMock.VerifyOnce(x => x.LeftHandToRightHand(It.IsAny<Action>()));
            VerifyNoOtherViewCalls();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        void HoldRightHand(bool shouldTestCallback)
        {
            if (shouldTestCallback)
                item.HoldRightHand(callback);
            else
                item.HoldRightHand();
        }
    }
}
