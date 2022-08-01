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
        public void Drop_ShouldSpawnAndDrop_WhenItemNotInHand(bool shouldTestCallback)
        {
            Drop(shouldTestCallback);

            itemViewMock.VerifyOnce(
                x => x.SpawnAndDrop(It.IsAny<Action>()));

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Drop_ShouldDropFromLeftHand_WhenItemInLeftHand(bool shouldTestCallback)
        {
            ArrangeItemInLeftHand();

            Drop(shouldTestCallback);

            itemViewMock.VerifyOnce(
                x => x.DropLeftHand(It.IsAny<Action>()));

            VerifyItemWasUnsetFromLeftHand();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Drop_ShouldDropFromRightHand_WhenItemInRightHand(bool shouldTestCallback)
        {
            ArrangeItemInRightHand();

            Drop(shouldTestCallback);

            itemViewMock.VerifyOnce(
                x => x.DropRightHand(It.IsAny<Action>()));

            VerifyItemWasUnsetFromRightHand();

            if (shouldTestCallback)
                VerifyCallbackWasInvoked();
        }

        void Drop(bool shouldTestCallback)
        {
            if (shouldTestCallback)
                item.Drop(callback);
            else
                item.Drop();
        }
    }
}
