using Moq;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Items.HolsterForItem;
using System;

namespace Strawhenge.Inventory.Tests.UnitTests
{
    public partial class HolsterForItem_Tests
    {
        readonly HolsterForItem holsterForItem;
        readonly Mock<IItem> itemMock;
        readonly Mock<IHolster> holsterMock;
        readonly Mock<IHolsterForItemView> holsterItemViewMock;
        readonly AssertableCallback callback;

        public HolsterForItem_Tests()
        {
            itemMock = new Mock<IItem>();
            holsterItemViewMock = new Mock<IHolsterForItemView>();
            holsterItemViewMock.Setup(x => x.DrawLeftHand(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());
            holsterItemViewMock.Setup(x => x.DrawRightHand(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());
            holsterItemViewMock.Setup(x => x.Hide(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());
            holsterItemViewMock.Setup(x => x.PutAwayLeftHand(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());
            holsterItemViewMock.Setup(x => x.PutAwayRightHand(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());
            holsterItemViewMock.Setup(x => x.Show(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());

            holsterMock = new Mock<IHolster>();
            holsterMock.SetupGetNone(x => x.CurrentItem);

            holsterForItem = new HolsterForItem(
                itemMock.Object,
                holsterMock.Object,
                holsterItemViewMock.Object);

            callback = new AssertableCallback();
        }

        void ArrangeInHand()
        {
            itemMock.SetupGet(x => x.IsInHand)
                .Returns(true);
        }

        void ArrangeEquipped()
        {
            holsterMock.Setup(x => x.IsCurrentItem(itemMock.Object))
                .Returns(true);
        }

        void VerifyItemWasSetToContainer()
        {
            holsterMock.VerifyOnce(
                x => x.SetItem(itemMock.Object));
        }

        void VerifyItemWasUnsetFromContainer()
        {
            holsterMock.VerifyOnce(
                x => x.UnsetItem());
        }

        void VerifyNoOtherViewCalls()
        {
            holsterItemViewMock.VerifyAdd(x => x.Released += It.IsAny<Action>(), Times.AtMostOnce());
            holsterItemViewMock.VerifyNoOtherCalls();
        }

        void VerifyItemWasUnequippedFromAnyOtherHolsters()
        {
            itemMock.VerifyOnce(
                x => x.UnequipFromHolster(It.IsAny<Action>()));
        }

        void VerifyCallbackWasInvoked()
        {
            callback.AssertCalledOnce();
        }
    }
}
