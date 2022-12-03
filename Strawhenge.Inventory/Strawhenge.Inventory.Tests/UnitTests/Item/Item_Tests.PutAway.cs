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
        public void PutAway_ShouldDoNothing_WhenNotInHand(bool shouldTestCallback)
        {
            PutAway(shouldTestCallback);

            VerifyNoOtherViewCalls();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void PutAway_ShouldPutAwayLeftHand_WhenItemInLeftHand(bool shouldTestCallback)
        {
            ArrangeItemInLeftHand();

            PutAway(shouldTestCallback);

            _itemViewMock.VerifyOnce(
                x => x.PutAwayLeftHand(It.IsAny<Action>()));

            VerifyItemWasUnsetFromLeftHand();
            VerifyNoOtherViewCalls();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void PutAway_ShouldPutAwayRightHand_WhenItemInRightHand(bool shouldTestCallback)
        {
            ArrangeItemInRightHand();

            PutAway(shouldTestCallback);

            _itemViewMock.VerifyOnce(
                x => x.PutAwayRightHand(It.IsAny<Action>()));

            VerifyItemWasUnsetFromRightHand();
            VerifyNoOtherViewCalls();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void PutAway_ShouldPutInHolsterFromLeftHand_WhenInLeftHand_AndEquippedToHolster(bool shouldTestCallback)
        {
            ArrangeItemInLeftHand();
            var holsterViewMock = ArrangeEquippedToHolster();

            PutAway(shouldTestCallback);

            holsterViewMock.VerifyOnce(
                x => x.PutAwayLeftHand(It.IsAny<Action>()));

            holsterViewMock.VerifyNoOtherCalls();

            VerifyItemWasUnsetFromLeftHand();
            VerifyNoOtherViewCalls();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void PutAway_ShouldPutInHolsterFromRightHand_WhenInRightHand_AndEquippedToHolster(bool shouldTestCallback)
        {
            ArrangeItemInRightHand();
            var holsterViewMock = ArrangeEquippedToHolster();

            PutAway(shouldTestCallback);

            holsterViewMock.VerifyOnce(
                x => x.PutAwayRightHand(It.IsAny<Action>()));

            holsterViewMock.VerifyNoOtherCalls();

            VerifyItemWasUnsetFromRightHand();
            VerifyNoOtherViewCalls();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        void PutAway(bool shouldTestCallback)
        {
            if (shouldTestCallback)
                _item.PutAway(_callback);
            else
                _item.PutAway();
        }
    }
}
