using Moq;
using System;
using Xunit;

namespace Strawhenge.Inventory.Tests.UnitTests
{
    public partial class HolsterForItem_Tests
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Unequip_ShouldDoNothing_WhenNotEquipped_AndNotInHand(bool shouldTestCallback)
        {
            Unequip(shouldTestCallback);

            VerifyNoOtherViewCalls();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Unequip_ShouldDoNothing_WhenNotEquipped_AndInHand(bool shouldTestCallback)
        {
            ArrangeInHand();

            Unequip(shouldTestCallback);

            VerifyNoOtherViewCalls();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Unequip_ShouldHide_WhenEquipped_AndNotInHand(bool shouldTestCallback)
        {
            ArrangeEquipped();

            Unequip(shouldTestCallback);

            _holsterItemViewMock.VerifyOnce(
                x => x.Hide(It.IsAny<Action>()));

            VerifyNoOtherViewCalls();
            VerifyItemWasUnsetFromContainer();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Unequip_ShouldUnsetFromContainer_WhenEquipped_AndInHand(bool shouldTestCallback)
        {
            ArrangeEquipped();
            ArrangeInHand();

            Unequip(shouldTestCallback);

            VerifyNoOtherViewCalls();
            VerifyItemWasUnsetFromContainer();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        void Unequip(bool shouldTestCallback)
        {
            if (shouldTestCallback)
                _holsterForItem.Unequip(_callback);
            else
                _holsterForItem.Unequip();
        }
    }
}
