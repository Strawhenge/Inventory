using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Items.HolsterForItem;
using System;
using System.Collections.Generic;

namespace Strawhenge.Inventory.Items
{
    public class Item : IItem
    {
        private readonly IHands hands;
        private readonly IItemView itemView;
        private readonly ItemSize size;
        private readonly IHolstersForItem holsters;

        public event Action<IItem> Dropped;

        public Item(
            string name,
            IHands hands,
            IItemView itemView,
            ItemSize size,
            Func<IItem, IHolstersForItem> getHolstersForItem)
        {
            Name = name;

            this.hands = hands;
            this.itemView = itemView;
            this.size = size;

            holsters = getHolstersForItem(this);

            itemView.Released += OnRemoved;
        }

        public string Name { get; }

        public IEnumerable<IHolsterForItem> Holsters => holsters;

        public bool IsInHand => IsInLeftHand() || IsInRightHand();

        public bool IsTwoHanded => size.IsTwoHanded;

        public ClearFromHandsPreference ClearFromHandsPreference { get; set; } = ClearFromHandsPreference.Disappear;

        public void Drop(Action callback = null)
        {
            if (IsInLeftHand())
            {
                hands.UnsetItemLeftHand();
                itemView.DropLeftHand(callback);
            }
            else if (IsInRightHand())
            {
                hands.UnsetItemRightHand();
                itemView.DropRightHand(callback);
            }
            else
            {
                itemView.SpawnAndDrop(callback);
            }

            Dropped?.Invoke(this);
        }

        public void HoldLeftHand(Action callback = null)
        {
            if (IsInLeftHand())
            {
                callback?.Invoke();
                return;
            }

            ClearLeftHand();

            if (IsInRightHand())
            {
                hands.UnsetItemRightHand();
                hands.SetItemLeftHand(this);

                itemView.RightHandToLeftHand(callback);
                return;
            }

            if (IsTwoHanded)
                ClearRightHand();

            if (IsEquippedToHolster(out var holsterItemView))
            {
                hands.SetItemLeftHand(this);

                holsterItemView.DrawLeftHand(callback);
                return;
            }

            hands.SetItemLeftHand(this);
            itemView.DrawLeftHand(callback);
        }

        public void HoldRightHand(Action callback = null)
        {
            if (IsInRightHand())
            {
                callback?.Invoke();
                return;
            }

            ClearRightHand();

            if (IsInLeftHand())
            {
                hands.UnsetItemLeftHand();
                hands.SetItemRightHand(this);

                itemView.LeftHandToRightHand(callback);
                return;
            }

            if (IsTwoHanded)
                ClearLeftHand();

            if (IsEquippedToHolster(out var holsterItemView))
            {
                hands.SetItemRightHand(this);

                holsterItemView.DrawRightHand(callback);
                return;
            }

            hands.SetItemRightHand(this);
            itemView.DrawRightHand(callback);
        }

        public void PutAway(Action callback = null)
        {
            if (IsInLeftHand())
            {
                hands.UnsetItemLeftHand();

                if (IsEquippedToHolster(out var holsterItemView))
                    holsterItemView.PutAwayLeftHand(callback);
                else
                    itemView.PutAwayLeftHand(callback);

                return;
            }

            if (IsInRightHand())
            {
                hands.UnsetItemRightHand();

                if (IsEquippedToHolster(out var holsterItemView))
                    holsterItemView.PutAwayRightHand(callback);
                else
                    itemView.PutAwayRightHand(callback);

                return;
            }

            callback?.Invoke();
        }

        public void UnequipFromHolster(Action callback = null)
        {
            if (holsters.IsEquippedToHolster(out IHolsterForItem holster))
            {
                holster.Unequip(callback);
                return;
            }

            callback?.Invoke();
        }

        public void ClearFromHands(Action callback = null)
        {
            if (IsInLeftHand())
            {
                hands.UnsetItemLeftHand();

                if (IsEquippedToHolster(out var holster))
                    holster.PutAwayLeftHand(callback);
                else
                    ClearFromHandsPreference.PerformClearLeftHand(itemView, callback);

                return;
            }

            if (IsInRightHand())
            {
                hands.UnsetItemRightHand();

                if (IsEquippedToHolster(out var holster))
                    holster.PutAwayRightHand(callback);
                else
                    ClearFromHandsPreference.PerformClearRightHand(itemView, callback);

                return;
            }

            callback?.Invoke();
        }

        void ClearLeftHand()
        {
            if (hands.HasTwoHandedItem(out var twoHandedItem) && twoHandedItem != this)
            {
                twoHandedItem.ClearFromHands();
                return;
            }

            hands.ItemInLeftHand.Do(x => x.ClearFromHands());
        }

        void ClearRightHand()
        {
            if (hands.HasTwoHandedItem(out var twoHandedItem) && twoHandedItem != this)
            {
                twoHandedItem.ClearFromHands();
                return;
            }

            hands.ItemInRightHand.Do(x => x.ClearFromHands());
        }

        bool IsInLeftHand() => hands.IsInLeftHand(this);

        bool IsInRightHand() => hands.IsInRightHand(this);

        bool IsEquippedToHolster(out IHolsterForItemView holsterItemView) =>
            holsters.IsEquippedToHolster(out holsterItemView);

        void OnRemoved()
        {
            if (IsInLeftHand())
                hands.UnsetItemLeftHand();

            if (IsInRightHand())
                hands.UnsetItemRightHand();         
        }
    }
}
