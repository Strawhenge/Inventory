using System;
using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Effects;
using Strawhenge.Inventory.Items.Consumables;
using Strawhenge.Inventory.Items.Holsters;
using Strawhenge.Inventory.Items.Storables;
using Strawhenge.Inventory.Procedures;

namespace Strawhenge.Inventory.Items
{
    public class Item
    {
        readonly Hands _hands;
        readonly ItemSize _size;
        readonly ItemProcedureScheduler _procedureScheduler;

        public Item(
            ItemData data,
            Context context,
            Hands hands,
            IItemProcedures procedures,
            ProcedureQueue procedureQueue,
            bool isTransient = false)
        {
            Name = data.Name;
            Data = data;
            Context = context;
            IsTransient = isTransient;

            _hands = hands;
            _procedureScheduler = new ItemProcedureScheduler(procedures, procedureQueue);
            _size = data.Size;
            Holsters = HolstersForItem.None;
        }

        public string Name { get; }

        public ItemData Data { get; }

        public Context Context { get; }

        public HolstersForItem Holsters { get; private set; }

        public Maybe<Consumable> Consumable { get; private set; } = Maybe.None<Consumable>();

        public Maybe<Storable> Storable { get; private set; } = Maybe.None<Storable>();

        public bool IsInHand => IsInLeftHand() || IsInRightHand();

        public bool IsTwoHanded => _size == ItemSize.TwoHanded;

        public bool IsTransient { get; }

        public bool IsInStorage => Storable
            .Map(x => x.IsStored)
            .Reduce(() => false);

        public void SetupHolsters(HolstersForItem holsters) => Holsters = holsters;

        public void SetupConsumable(IConsumableProcedures procedures, IEnumerable<Effect> effects) =>
            Consumable = new Consumable(this, effects, procedures, _procedureScheduler.Queue);

        public void SetupStorable(StoredItems storage, int weight) =>
            Storable = new Storable(this, storage, weight);

        public void Drop(Action callback = null)
        {
            if (IsInLeftHand())
            {
                UnequipFromHolster();
                _hands.UnsetItemLeftHand();

                if (IsTransient)
                    _procedureScheduler.DisappearLeftHand();
                else
                    _procedureScheduler.DropLeftHand(callback);
            }
            else if (IsInRightHand())
            {
                UnequipFromHolster();
                _hands.UnsetItemRightHand();

                if (IsTransient)
                    _procedureScheduler.DisappearRightHand();
                else
                    _procedureScheduler.DropRightHand(callback);
            }
            else if (Holsters.IsEquippedToHolster(out HolsterForItem holster))
            {
                holster.Drop(callback);
            }
            else
            {
                _procedureScheduler.SpawnAndDrop(callback);
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

                _procedureScheduler.RightHandToLeftHand(callback);
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
                _procedureScheduler.DrawLeftHand(callback);
            else
                _procedureScheduler.AppearLeftHand();
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

                _procedureScheduler.LeftHandToRightHand(callback);
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
                _procedureScheduler.DrawRightHand(callback);
            else
                _procedureScheduler.AppearRightHand();
        }

        public void PutAway(Action callback = null)
        {
            if (IsInLeftHand())
            {
                _hands.UnsetItemLeftHand();

                if (IsEquippedToHolster(out var holsterItemView))
                    holsterItemView.PutAwayLeftHand(callback);
                else
                    _procedureScheduler.PutAwayLeftHand(callback);

                return;
            }

            if (IsInRightHand())
            {
                _hands.UnsetItemRightHand();

                if (IsEquippedToHolster(out var holsterItemView))
                    holsterItemView.PutAwayRightHand(callback);
                else
                    _procedureScheduler.PutAwayRightHand(callback);

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
                    _procedureScheduler.PutAwayLeftHand(callback);
                else if (IsTransient)
                    _procedureScheduler.DisappearLeftHand();
                else
                    _procedureScheduler.DropLeftHand();

                return;
            }

            if (IsInRightHand())
            {
                _hands.UnsetItemRightHand();

                if (IsEquippedToHolster(out var holster))
                    holster.PutAwayRightHand(callback);
                else if (IsInStorage)
                    _procedureScheduler.PutAwayRightHand(callback);
                else if (IsTransient)
                    _procedureScheduler.DisappearRightHand();
                else
                    _procedureScheduler.DropRightHand();

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
                _procedureScheduler.DisappearLeftHand();
            }
            else if (IsInRightHand())
            {
                _hands.UnsetItemRightHand();
                _procedureScheduler.DisappearRightHand();
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

                otherItem._procedureScheduler.DisappearRightHand();
                _procedureScheduler.LeftHandToRightHand();
                otherItem._procedureScheduler.AppearLeftHand();
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

                otherItem._procedureScheduler.DisappearLeftHand();
                _procedureScheduler.RightHandToLeftHand();
                otherItem._procedureScheduler.AppearRightHand();
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

        bool IsEquippedToHolster(out HolsterForItemProcedureScheduler procedureScheduler) =>
            Holsters.IsEquippedToHolster(out procedureScheduler);

        public override string ToString()
        {
            return Name;
        }
    }
}