using Moq;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Items.HolsterForItem;
using System;
using FunctionalUtilities;
using Strawhenge.Inventory.Items.Consumables;
using Strawhenge.Inventory.Items.Storables;

namespace Strawhenge.Inventory.Tests.UnitTests
{
    public partial class Item_Tests
    {
        const string ItemName = "Test Item";

        readonly Item _item;
        readonly Mock<IHands> _handsMock;
        readonly Mock<IItemView> _itemViewMock;
        readonly Mock<IHolstersForItem> _holstersMock;
        readonly Mock<ItemSize> _itemSizeMock;
        readonly AssertableCallback _callback;

        public Item_Tests()
        {
            _handsMock = new Mock<IHands>();
            _handsMock.SetupGetNone(x => x.ItemInLeftHand);
            _handsMock.SetupGetNone(x => x.ItemInRightHand);

            _itemViewMock = new Mock<IItemView>();
            _itemViewMock.Setup(x => x.Disappear(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());
            _itemViewMock.Setup(x => x.DrawLeftHand(It.IsAny<Action>()))
                .Callback<Action>(callback => callback?.Invoke());
            _itemViewMock.Setup(x => x.DrawRightHand(It.IsAny<Action>()))
                .Callback<Action>(callback => callback?.Invoke());
            _itemViewMock.Setup(x => x.DropLeftHand(It.IsAny<Action>()))
                .Callback<Action>(callback => callback?.Invoke());
            _itemViewMock.Setup(x => x.DropRightHand(It.IsAny<Action>()))
                .Callback<Action>(callback => callback?.Invoke());
            _itemViewMock.Setup(x => x.LeftHandToRightHand(It.IsAny<Action>()))
                .Callback<Action>(callback => callback?.Invoke());
            _itemViewMock.Setup(x => x.PutAwayLeftHand(It.IsAny<Action>()))
                .Callback<Action>(callback => callback?.Invoke());
            _itemViewMock.Setup(x => x.PutAwayRightHand(It.IsAny<Action>()))
                .Callback<Action>(callback => callback?.Invoke());
            _itemViewMock.Setup(x => x.RightHandToLeftHand(It.IsAny<Action>()))
                .Callback<Action>(callback => callback?.Invoke());
            _itemViewMock.Setup(x => x.SpawnAndDrop(It.IsAny<Action>()))
                .Callback<Action>(callback => callback?.Invoke());

            _holstersMock = new Mock<IHolstersForItem>();
            _itemSizeMock = new Mock<ItemSize>();

            _item = new Item(
                ItemName,
                _handsMock.Object,
                _itemViewMock.Object,
                _itemSizeMock.Object,
                _ => _holstersMock.Object,
                _ => Maybe.None<IConsumable>(),
                _ => Maybe.None<IStorable>());

            _callback = new AssertableCallback();
        }

        void ArrangeItemInLeftHand()
        {
            _handsMock
                .Setup(x => x.IsInLeftHand(_item))
                .Returns(true);
        }

        void ArrangeTwoHandedItemInLeftHand()
        {
            ArrangeItemInLeftHand();
            ArrangeTwoHanded();

            IItem item = _item;
            _handsMock.Setup(x => x.HasTwoHandedItem(out item)).Returns(true);
        }

        void ArrangeItemInRightHand()
        {
            _handsMock
                .Setup(x => x.IsInRightHand(_item))
                .Returns(true);
        }

        void ArrangeTwoHandedItemInRightHand()
        {
            ArrangeItemInRightHand();
            ArrangeTwoHanded();

            IItem item = _item;
            _handsMock.Setup(x => x.HasTwoHandedItem(out item)).Returns(true);
        }

        Mock<IHolsterForItemView> ArrangeEquippedToHolster()
        {
            var viewMock = new Mock<IHolsterForItemView>();
            viewMock.Setup(x => x.DrawLeftHand(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());
            viewMock.Setup(x => x.DrawRightHand(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());
            viewMock.Setup(x => x.Hide(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());
            viewMock.Setup(x => x.PutAwayLeftHand(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());
            viewMock.Setup(x => x.PutAwayRightHand(It.IsAny<Action>()))
                .Callback<Action>(callback => callback?.Invoke());
            viewMock.Setup(x => x.Show(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());

            var holsterMock = new Mock<IHolsterForItem>();
            holsterMock.Setup(x => x.Equip(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());
            holsterMock.Setup(x => x.Unequip(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());

            holsterMock.SetupGet(x => x.IsEquipped).Returns(true);
            holsterMock.Setup(x => x.GetView()).Returns(viewMock.Object);

            IHolsterForItem holster = holsterMock.Object;

            _holstersMock
                .Setup(x => x.IsEquippedToHolster(out holster))
                .Returns(true);

            IHolsterForItemView view = viewMock.Object;

            _holstersMock
                .Setup(x => x.IsEquippedToHolster(out view))
                .Returns(true);

            return viewMock;
        }

        void ArrangeClearFromHandsSetToDrop()
        {
            _item.ClearFromHandsPreference = ClearFromHandsPreference.Drop;
        }

        void ArrangeClearFromHandsSetToPutAway()
        {
            _item.ClearFromHandsPreference = ClearFromHandsPreference.PutAway;
        }

        void ArrangeTwoHanded()
        {
            _itemSizeMock.SetupGet(x => x.IsTwoHanded).Returns(true);
        }

        Mock<IItem> ArrangeOtherItemInRightHand(bool isTwoHanded = false)
        {
            var otherItemMock = new Mock<IItem>();
            otherItemMock.SetupGet(x => x.IsTwoHanded).Returns(isTwoHanded);
            var otherItem = otherItemMock.Object;

            _handsMock.SetupGetSome(x => x.ItemInRightHand, otherItem);

            if (isTwoHanded)
                _handsMock.Setup(x => x.HasTwoHandedItem(out otherItem)).Returns(true);

            return otherItemMock;
        }

        Mock<IItem> ArrangeOtherItemInLeftHand(bool isTwoHanded = false)
        {
            var otherItemMock = new Mock<IItem>();
            otherItemMock.SetupGet(x => x.IsTwoHanded).Returns(isTwoHanded);
            var otherItem = otherItemMock.Object;

            _handsMock.SetupGetSome(x => x.ItemInLeftHand, otherItemMock.Object);

            if (isTwoHanded)
                _handsMock.Setup(x => x.HasTwoHandedItem(out otherItem)).Returns(true);

            return otherItemMock;
        }

        void VerifyItemWasSetToLeftHand()
        {
            _handsMock.VerifyOnce(
                x => x.SetItemLeftHand(_item));
        }

        void VerifyItemWasSetToRightHand()
        {
            _handsMock.VerifyOnce(
                x => x.SetItemRightHand(_item));
        }

        void VerifyItemWasUnsetFromLeftHand()
        {
            _handsMock.VerifyOnce(
                x => x.UnsetItemLeftHand());
        }

        void VerifyItemWasUnsetFromRightHand()
        {
            _handsMock.VerifyOnce(
                x => x.UnsetItemRightHand());
        }

        void VerifyNoOtherViewCalls()
        {
            _itemViewMock.VerifyAdd(x => x.Released += It.IsAny<Action>(), Times.AtMostOnce());
            _itemViewMock.VerifyNoOtherCalls();
        }

        void VerifyCallbackWasInvoked()
        {
            _callback.AssertCalledOnce();
        }
    }
}