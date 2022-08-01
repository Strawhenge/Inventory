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
        public void ClearFromHands_ShouldDoNothingWhenNotInHand(bool shouldTestCallback)
        {
            ClearFromHands(shouldTestCallback);

            VerifyNoOtherViewCalls();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ClearFromHands_ShouldDropFromLeftHand_WhenInLeftHand_AndPreferenceSetToDrop(bool shouldTestCallback)
        {
            ArrangeItemInLeftHand();
            ArrangeClearFromHandsSetToDrop();

            ClearFromHands(shouldTestCallback);

            itemViewMock.VerifyOnce(x => x.DropLeftHand(It.IsAny<Action>()));
            VerifyNoOtherViewCalls();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ClearFromHands_ShouldDropFromRightHand_WhenInRightHand_AndPreferenceSetToDrop(bool shouldTestCallback)
        {
            ArrangeItemInRightHand();
            ArrangeClearFromHandsSetToDrop();

            ClearFromHands(shouldTestCallback);

            itemViewMock.VerifyOnce(x => x.DropRightHand(It.IsAny<Action>()));
            VerifyNoOtherViewCalls();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ClearFromHands_ShouldPutAwayFromLeftHand_WhenInLeftHand_AndPreferenceSetToPutAway(bool shouldTestCallback)
        {
            ArrangeItemInLeftHand();
            ArrangeClearFromHandsSetToPutAway();

            ClearFromHands(shouldTestCallback);

            itemViewMock.VerifyOnce(x => x.PutAwayLeftHand(It.IsAny<Action>()));
            VerifyNoOtherViewCalls();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ClearFromHands_ShouldPutAwayFromRightHand_WhenInRightHand_AndPreferenceSetToPutAway(bool shouldTestCallback)
        {
            ArrangeItemInRightHand();
            ArrangeClearFromHandsSetToPutAway();

            ClearFromHands(shouldTestCallback);

            itemViewMock.VerifyOnce(x => x.PutAwayRightHand(It.IsAny<Action>()));
            VerifyNoOtherViewCalls();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ClearFromHands_ShouldHolsterFromLeftHand_WhenInLeftHand_AndEquippedToHolster(bool shouldTestCallback)
        {
            ArrangeItemInLeftHand();
            var holsterViewMock = ArrangeEquippedToHolster();

            ClearFromHands(shouldTestCallback);

            holsterViewMock.VerifyOnce(x => x.PutAwayLeftHand(It.IsAny<Action>()));
            VerifyNoOtherViewCalls();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ClearFromHands_ShouldHolsterFromRightHand_WhenInRightHand_AndEquippedToHolster(bool shouldTestCallback)
        {
            ArrangeItemInRightHand();
            var holsterViewMock = ArrangeEquippedToHolster();

            ClearFromHands(shouldTestCallback);

            holsterViewMock.VerifyOnce(x => x.PutAwayRightHand(It.IsAny<Action>()));
            VerifyNoOtherViewCalls();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        void ClearFromHands(bool shouldTestCallback)
        {
            if (shouldTestCallback)
                item.ClearFromHands(callback);
            else
                item.ClearFromHands();
        }
    }
}
