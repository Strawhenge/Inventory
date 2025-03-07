using System;
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
        readonly Hands _hands;
        readonly IItemView _itemView;
        readonly ItemSize _size;

        public Item(
            string name,
            Hands hands,
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

        public Maybe<Storable> Storable { get; private set; } = Maybe.None<Storable>();

        public bool IsInHand => IsInLeftHand() || IsInRightHand();

        public bool IsTwoHanded => _size == ItemSize.TwoHanded;

        public bool IsInStorage => Storable
            .Map(x => x.IsStored)
            .Reduce(() => false);

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

            if (IsInStorage)
                _itemView.DrawLeftHand(callback);
            else
                _itemView.AppearLeftHand();
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

            if (IsInStorage)
                _itemView.DrawRightHand(callback);
            else
                _itemView.AppearRightHand();
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
                else if (IsInStorage)
                    _itemView.PutAwayLeftHand(callback);
                else
                    _itemView.DropLeftHand();

                return;
            }

            if (IsInRightHand())
            {
                _hands.UnsetItemRightHand();

                if (IsEquippedToHolster(out var holster))
                    holster.PutAwayRightHand(callback);
                else if (IsInStorage)
                    _itemView.PutAwayRightHand(callback);
                else
                    _itemView.DropRightHand();

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
                _itemView.DisappearLeftHand();
            }
            else if (IsInRightHand())
            {
                _hands.UnsetItemRightHand();
                _itemView.DisappearRightHand();
            }

            Storable.Do(x => x.RemoveFromStorage());
        }

        public void SwapHands()
        {
            if (IsInLeftHand())
            {
                if (!_hands.RightHand.CurrentItem.HasSome(out var otherItem))
                {
                    HoldRightHand();
                    return;
                }

                _hands.RightHand.SetItem(this);
                _hands.LeftHand.SetItem(otherItem);

                otherItem._itemView.DisappearRightHand();
                _itemView.LeftHandToRightHand();
                otherItem._itemView.AppearLeftHand();
            }
            else if (IsInRightHand())
            {
                if (!_hands.LeftHand.CurrentItem.HasSome(out var otherItem))
                {
                    HoldLeftHand();
                    return;
                }
               
                _hands.LeftHand.SetItem(this);
                _hands.RightHand.SetItem(otherItem);

                otherItem._itemView.DisappearLeftHand();
                _itemView.RightHandToLeftHand();
                otherItem._itemView.AppearRightHand();
            }
        }

        internal void DisappearFromHolster(Action callback = null)
        {
            if (Holsters.IsEquippedToHolster(out HolsterForItem holster))
            {
                holster.Disappear(callback);
                return;
            }

            callback?.Invoke();
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