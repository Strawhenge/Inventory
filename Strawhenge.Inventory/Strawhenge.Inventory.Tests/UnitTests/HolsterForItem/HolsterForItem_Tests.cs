using Moq;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Items.HolsterForItem;
using System;
using Xunit;

namespace Strawhenge.Inventory.Tests.UnitTests
{
    public partial class HolsterForItem_Tests
    {
        readonly HolsterForItem _holsterForItem;
        readonly Mock<IItem> _itemMock;
        readonly ItemContainer _itemContainer;
        readonly Mock<IHolsterForItemView> _holsterItemViewMock;
        readonly AssertableCallback _callback;

        public HolsterForItem_Tests()
        {
            _itemMock = new Mock<IItem>();
            _holsterItemViewMock = new Mock<IHolsterForItemView>();
            _holsterItemViewMock.Setup(x => x.DrawLeftHand(It.IsAny<Action>()))
                .Callback<Action>(callback => callback?.Invoke());
            _holsterItemViewMock.Setup(x => x.DrawRightHand(It.IsAny<Action>()))
                .Callback<Action>(callback => callback?.Invoke());
            _holsterItemViewMock.Setup(x => x.Hide(It.IsAny<Action>()))
                .Callback<Action>(callback => callback?.Invoke());
            _holsterItemViewMock.Setup(x => x.PutAwayLeftHand(It.IsAny<Action>()))
                .Callback<Action>(callback => callback?.Invoke());
            _holsterItemViewMock.Setup(x => x.PutAwayRightHand(It.IsAny<Action>()))
                .Callback<Action>(callback => callback?.Invoke());
            _holsterItemViewMock.Setup(x => x.Show(It.IsAny<Action>()))
                .Callback<Action>(callback => callback?.Invoke());

            _itemContainer = new ItemContainer(nameof(HolsterForItem_Tests));

            _holsterForItem = new HolsterForItem(
                _itemMock.Object,
                _itemContainer,
                _holsterItemViewMock.Object);

            _callback = new AssertableCallback();
        }

        void ArrangeInHand()
        {
            _itemMock.SetupGet(x => x.IsInHand)
                .Returns(true);
        }

        void ArrangeEquipped()
        {
            _itemContainer.SetItem(_itemMock.Object);
        }

        void VerifyItemWasSetToContainer()
        {
            Assert.True(
                _itemContainer.IsCurrentItem(_itemMock.Object));
        }

        void VerifyItemWasUnsetFromContainer()
        {
            Assert.False(
                _itemContainer.IsCurrentItem(_itemMock.Object));
        }

        void VerifyNoOtherViewCalls()
        {
            _holsterItemViewMock.VerifyAdd(x => x.Released += It.IsAny<Action>(), Times.AtMostOnce());
            _holsterItemViewMock.VerifyNoOtherCalls();
        }

        void VerifyItemWasUnequippedFromAnyOtherHolsters()
        {
            _itemMock.VerifyOnce(
                x => x.UnequipFromHolster(It.IsAny<Action>()));
        }

        void VerifyCallbackWasInvoked()
        {
            _callback.AssertCalledOnce();
        }
    }
}