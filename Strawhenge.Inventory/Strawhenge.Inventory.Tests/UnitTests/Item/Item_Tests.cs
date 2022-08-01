using Moq;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Items.HolsterForItem;
using System;

namespace Strawhenge.Inventory.Tests.UnitTests
{
    public partial class Item_Tests
    {
        const string itemName = "Test Item";

        readonly Item item;
        readonly Mock<IHands> handsMock;
        readonly Mock<IItemView> itemViewMock;
        readonly Mock<IHolstersForItem> holstersMock;
        readonly Mock<ItemSize> itemSizeMock;
        readonly AssertableCallback callback;

        public Item_Tests()
        {
            handsMock = new Mock<IHands>();
            handsMock.SetupGetNone(x => x.ItemInLeftHand);
            handsMock.SetupGetNone(x => x.ItemInRightHand);

            itemViewMock = new Mock<IItemView>();
            itemViewMock.Setup(x => x.Disappear(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());
            itemViewMock.Setup(x => x.DrawLeftHand(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());
            itemViewMock.Setup(x => x.DrawRightHand(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());
            itemViewMock.Setup(x => x.DropLeftHand(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());
            itemViewMock.Setup(x => x.DropRightHand(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());
            itemViewMock.Setup(x => x.LeftHandToRightHand(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());
            itemViewMock.Setup(x => x.PutAwayLeftHand(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());
            itemViewMock.Setup(x => x.PutAwayRightHand(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());
            itemViewMock.Setup(x => x.RightHandToLeftHand(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());
            itemViewMock.Setup(x => x.SpawnAndDrop(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());

            holstersMock = new Mock<IHolstersForItem>();
            itemSizeMock = new Mock<ItemSize>();

            item = new Item(
                itemName,
                handsMock.Object,
                itemViewMock.Object,
                itemSizeMock.Object,
                _ => holstersMock.Object);

            callback = new AssertableCallback();
        }

        void ArrangeItemInLeftHand()
        {
            handsMock
                .Setup(x => x.IsInLeftHand(item))
                .Returns(true);
        }

        void ArrangeTwoHandedItemInLeftHand()
        {
            ArrangeItemInLeftHand();
            ArrangeTwoHanded();

            IItem item = this.item;
            handsMock.Setup(x => x.HasTwoHandedItem(out item)).Returns(true);
        }

        void ArrangeItemInRightHand()
        {
            handsMock
                .Setup(x => x.IsInRightHand(item))
                .Returns(true);
        }

        void ArrangeTwoHandedItemInRightHand()
        {
            ArrangeItemInRightHand();
            ArrangeTwoHanded();

            IItem item = this.item;
            handsMock.Setup(x => x.HasTwoHandedItem(out item)).Returns(true);
        }

        Mock<IHolsterForItemView> ArrangeEquippedToHolster()
        {
            var viewMock = new Mock<IHolsterForItemView>();
            viewMock.Setup(x => x.DrawLeftHand(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());
            viewMock.Setup(x => x.DrawRightHand(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());
            viewMock.Setup(x => x.Hide(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());
            viewMock.Setup(x => x.PutAwayLeftHand(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());
            viewMock.Setup(x => x.PutAwayRightHand(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());
            viewMock.Setup(x => x.Show(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());

            var holsterMock = new Mock<IHolsterForItem>();
            holsterMock.Setup(x => x.Equip(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());
            holsterMock.Setup(x => x.Unequip(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());

            holsterMock.SetupGet(x => x.IsEquipped).Returns(true);
            holsterMock.Setup(x => x.GetView()).Returns(viewMock.Object);

            IHolsterForItem holster = holsterMock.Object;

            holstersMock
                .Setup(x => x.IsEquippedToHolster(out holster))
                .Returns(true);

            IHolsterForItemView view = viewMock.Object;

            holstersMock
                .Setup(x => x.IsEquippedToHolster(out view))
                .Returns(true);

            return viewMock;
        }

        void ArrangeClearFromHandsSetToDrop()
        {
            item.ClearFromHandsPreference = ClearFromHandsPreference.Drop;
        }

        void ArrangeClearFromHandsSetToPutAway()
        {
            item.ClearFromHandsPreference = ClearFromHandsPreference.PutAway;
        }

        void ArrangeTwoHanded()
        {
            itemSizeMock.SetupGet(x => x.IsTwoHanded).Returns(true);
        }

        Mock<IItem> ArrangeOtherItemInRightHand(bool isTwoHanded = false)
        {
            var otherItemMock = new Mock<IItem>();
            otherItemMock.SetupGet(x => x.IsTwoHanded).Returns(isTwoHanded);
            var otherItem = otherItemMock.Object;

            handsMock.SetupGetSome(x => x.ItemInRightHand, otherItem);

            if (isTwoHanded)
                handsMock.Setup(x => x.HasTwoHandedItem(out otherItem)).Returns(true);

            return otherItemMock;
        }

        Mock<IItem> ArrangeOtherItemInLeftHand(bool isTwoHanded = false)
        {
            var otherItemMock = new Mock<IItem>();
            otherItemMock.SetupGet(x => x.IsTwoHanded).Returns(isTwoHanded);
            var otherItem = otherItemMock.Object;

            handsMock.SetupGetSome(x => x.ItemInLeftHand, otherItemMock.Object);

            if (isTwoHanded)
                handsMock.Setup(x => x.HasTwoHandedItem(out otherItem)).Returns(true);

            return otherItemMock;
        }

        void VerifyItemWasSetToLeftHand()
        {
            handsMock.VerifyOnce(
                x => x.SetItemLeftHand(item));
        }

        void VerifyItemWasSetToRightHand()
        {
            handsMock.VerifyOnce(
                x => x.SetItemRightHand(item));
        }

        void VerifyItemWasUnsetFromLeftHand()
        {
            handsMock.VerifyOnce(
                x => x.UnsetItemLeftHand());
        }

        void VerifyItemWasUnsetFromRightHand()
        {
            handsMock.VerifyOnce(
                x => x.UnsetItemRightHand());
        }

        void VerifyNoOtherViewCalls()
        {
            itemViewMock.VerifyAdd(x => x.Released += It.IsAny<Action>(), Times.AtMostOnce());
            itemViewMock.VerifyNoOtherCalls();
        }

        void VerifyCallbackWasInvoked()
        {
            callback.AssertCalledOnce();
        }
    }
}
