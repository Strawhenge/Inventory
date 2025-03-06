﻿using System;
using System.Collections.Generic;
using System.Linq;
using FunctionalUtilities;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Effects;
using Strawhenge.Inventory.Items.Consumables;
using Strawhenge.Inventory.Items.Holsters;
using Strawhenge.Inventory.Items.Storables;

namespace Strawhenge.Inventory.Items
{
    public class Item
    {
        readonly IHands _hands;
        readonly IItemView _itemView;
        readonly ItemSize _size;

        public Item(
            string name,
            IHands hands,
            IItemView itemView,
            ItemSize size)
        {
            Name = name;

            _hands = hands;
            _itemView = itemView;
            _size = size;
            Holsters = HolstersForItem.None;

            itemView.Released += OnRemoved;
        }

        public string Name { get; }

        public HolstersForItem Holsters { get; private set; }

        public Maybe<Consumable> Consumable { get; private set; } = Maybe.None<Consumable>();

        public Maybe<IStorable> Storable { get; private set; } = Maybe.None<IStorable>();

        public bool IsInHand => IsInLeftHand() || IsInRightHand();

        public bool IsTwoHanded => _size.IsTwoHanded;

        public ClearFromHandsPreference ClearFromHandsPreference { get; set; } =
            ClearFromHandsPreference.Disappear;

        public ClearFromHolsterPreference ClearFromHolsterPreference
        {
            set
            {
                foreach (var holster in Holsters)
                    holster.ClearFromHolsterPreference = value;
            }
        }

        public void SetupHolsters(IEnumerable<(ItemContainer container, IHolsterForItemView view)> holsters) =>
            Holsters = new HolstersForItem(
                holsters.Select(x => new HolsterForItem(this, x.container, x.view)));

        public void SetupHolsters(HolstersForItem holsters) => Holsters = holsters;

        public void SetupConsumable(IConsumableView view, IEnumerable<Effect> effects) =>
            Consumable = new Consumable(this, view, effects);

        public void SetupStorable(StoredItems storage, int weight) =>
            Storable = new Storable(this, storage, weight);

        public void Drop(Action callback = null)
        {
            if (IsInLeftHand())
            {
                UnequipFromHolster();
                _hands.UnsetItemLeftHand();
                _itemView.DropLeftHand(callback);
            }
            else if (IsInRightHand())
            {
                UnequipFromHolster();
                _hands.UnsetItemRightHand();
                _itemView.DropRightHand(callback);
            }
            else if (Holsters.IsEquippedToHolster(out HolsterForItem holster))
            {
                holster.Drop(callback);
            }
            else
            {
                _itemView.SpawnAndDrop(callback);
            }

            Storable.Do(x => x.RemoveFromStorage());
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
                _hands.UnsetItemRightHand();
                _hands.SetItemLeftHand(this);

                _itemView.RightHandToLeftHand(callback);
                return;
            }

            if (IsTwoHanded)
                ClearRightHand();

            if (IsEquippedToHolster(out var holsterItemView))
            {
                _hands.SetItemLeftHand(this);

                holsterItemView.DrawLeftHand(callback);
                return;
            }

            _hands.SetItemLeftHand(this);
            _itemView.DrawLeftHand(callback);
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
                _hands.UnsetItemLeftHand();
                _hands.SetItemRightHand(this);

                _itemView.LeftHandToRightHand(callback);
                return;
            }

            if (IsTwoHanded)
                ClearLeftHand();

            if (IsEquippedToHolster(out var holsterItemView))
            {
                _hands.SetItemRightHand(this);

                holsterItemView.DrawRightHand(callback);
                return;
            }

            _hands.SetItemRightHand(this);
            _itemView.DrawRightHand(callback);
        }

        public void PutAway(Action callback = null)
        {
            if (IsInLeftHand())
            {
                _hands.UnsetItemLeftHand();

                if (IsEquippedToHolster(out var holsterItemView))
                    holsterItemView.PutAwayLeftHand(callback);
                else
                    _itemView.PutAwayLeftHand(callback);

                return;
            }

            if (IsInRightHand())
            {
                _hands.UnsetItemRightHand();

                if (IsEquippedToHolster(out var holsterItemView))
                    holsterItemView.PutAwayRightHand(callback);
                else
                    _itemView.PutAwayRightHand(callback);

                return;
            }

            callback?.Invoke();
        }

        public void UnequipFromHolster(Action callback = null)
        {
            if (Holsters.IsEquippedToHolster(out HolsterForItem holster))
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
                _hands.UnsetItemLeftHand();

                if (IsEquippedToHolster(out var holster))
                    holster.PutAwayLeftHand(callback);
                else
                    ClearFromHandsPreference.PerformClearLeftHand(_itemView, callback);

                return;
            }

            if (IsInRightHand())
            {
                _hands.UnsetItemRightHand();

                if (IsEquippedToHolster(out var holster))
                    holster.PutAwayRightHand(callback);
                else
                    ClearFromHandsPreference.PerformClearRightHand(_itemView, callback);

                return;
            }

            callback?.Invoke();
        }

        public void Discard()
        {
            if (Holsters.IsEquippedToHolster(out HolsterForItem holster))
                holster.Discard();

            if (IsInLeftHand())
            {
                _hands.UnsetItemLeftHand();
                _itemView.Disappear();
            }
            else if (IsInRightHand())
            {
                _hands.UnsetItemRightHand();
                _itemView.Disappear();
            }

            Storable.Do(x => x.RemoveFromStorage());
        }

        void ClearLeftHand()
        {
            if (_hands.HasTwoHandedItem(out var twoHandedItem) && twoHandedItem != this)
            {
                twoHandedItem.ClearFromHands();
                return;
            }

            _hands.ItemInLeftHand.Do(x => x.ClearFromHands());
        }

        void ClearRightHand()
        {
            if (_hands.HasTwoHandedItem(out var twoHandedItem) && twoHandedItem != this)
            {
                twoHandedItem.ClearFromHands();
                return;
            }

            _hands.ItemInRightHand.Do(x => x.ClearFromHands());
        }

        bool IsInLeftHand() => _hands.IsInLeftHand(this);

        bool IsInRightHand() => _hands.IsInRightHand(this);

        bool IsEquippedToHolster(out IHolsterForItemView holsterItemView) =>
            Holsters.IsEquippedToHolster(out holsterItemView);

        void OnRemoved()
        {
            if (IsInLeftHand())
                _hands.UnsetItemLeftHand();

            if (IsInRightHand())
                _hands.UnsetItemRightHand();

            Storable.Do(x => x.RemoveFromStorage());
        }

        public override string ToString()
        {
            return Name;
        }
    }
}