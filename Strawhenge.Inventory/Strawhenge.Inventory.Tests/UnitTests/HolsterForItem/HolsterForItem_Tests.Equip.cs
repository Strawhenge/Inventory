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
        public void Equip_ShouldShowInHolster_WhenNotEquipped_AndNotInHand(bool shouldTestCallback)
        {
            Equip(shouldTestCallback);

            _holsterItemViewMock.VerifyOnce(
                x => x.Show(It.IsAny<Action>()));

            VerifyNoOtherViewCalls();
            VerifyItemWasSetToContainer();
            VerifyItemWasUnequippedFromAnyOtherHolsters();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Equip_ShouldSetToContainer_WhenNotEquipped_AndInHand(bool shouldTestCallback)
        {
            ArrangeInHand();

            Equip(shouldTestCallback);

            VerifyNoOtherViewCalls();
            VerifyItemWasSetToContainer();
            VerifyItemWasUnequippedFromAnyOtherHolsters();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Equip_ShouldDoNothing_WhenEquipped_AndNotInHand(bool shouldTestCallback)
        {
            ArrangeEquipped();

            Equip(shouldTestCallback);

            VerifyNoOtherViewCalls();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Equip_ShouldDoNothing_WhenEquipped_AndInHand(bool shouldTestCallback)
        {
            ArrangeInHand();
            ArrangeEquipped();

            Equip(shouldTestCallback);

            VerifyNoOtherViewCalls();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        void Equip(bool shouldTestCallback)
        {
            if (shouldTestCallback)
                _holsterForItem.Equip(_callback);
            else
                _holsterForItem.Equip();
        }
    }
}
