using System;
using System.Collections.Generic;
using System.Linq;
using FunctionalUtilities;
using Strawhenge.Inventory.Effects;
using Strawhenge.Inventory.Procedures;

namespace Strawhenge.Inventory.Items
{
    public class InventoryItem
    {
        readonly Hands _hands;
        readonly ItemSize _size;
        readonly ItemProcedureScheduler _procedureScheduler;

        public InventoryItem(
            Item item,
            Context context,
            Hands hands,
            IItemProcedures procedures,
            ProcedureQueue procedureQueue,
            bool isTemporary = false)
        {
            Name = item.Name;
            Item = item;
            Context = context;
            IsTemporary = isTemporary;

            _hands = hands;
            _procedureScheduler = new ItemProcedureScheduler(procedures, procedureQueue);
            _size = item.Size;
            Holsters = InventoryItemHolsters.None;
        }

        public string Name { get; }

        public Item Item { get; }

        public Context Context { get; }

        public InventoryItemHolsters Holsters { get; private set; }

        public Maybe<Consumable> Consumable { get; private set; } = Maybe.None<Consumable>();

        public Maybe<Storable> Storable { get; private set; } = Maybe.None<Storable>();

        public bool IsInHand => IsInLeftHand || IsInRightHand;

        public bool IsInLeftHand => _hands.IsInLeftHand(this);

        public bool IsInRightHand => _hands.IsInRightHand(this);

        public bool IsTwoHanded => _size == ItemSize.TwoHanded;

        public bool IsTemporary { get; }

        public bool IsInStorage => Storable
            .Map(x => x.IsStored)
            .Reduce(() => false);

        public void Drop(Action callback = null)
        {
            if (IsInLeftHand)
            {
                UnequipFromHolster();
                _hands.UnsetItemLeftHand();

                if (IsTemporary)
                    _procedureScheduler.DisappearLeftHand();
                else
                    _procedureScheduler.DropLeftHand(callback);
            }
            else if (IsInRightHand)
            {
                UnequipFromHolster();
                _hands.UnsetItemRightHand();

                if (IsTemporary)
                    _procedureScheduler.DisappearRightHand();
                else
                    _procedureScheduler.DropRightHand(callback);
            }
            else if (Holsters.IsEquippedToHolster(out InventoryItemHolster holster))
            {
                holster.Drop(callback);
            }
            else
            {
                _procedureScheduler.SpawnAndDrop(callback);
            }

            Storable.Do(x => x.Discard());
        }

        public void HoldLeftHand(Action callback = null)
        {
            if (IsInLeftHand)
            {
                callback?.Invoke();
                return;
            }

            ClearLeftHand();

            if (IsInRightHand)
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
            if (IsInRightHand)
            {
                callback?.Invoke();
                return;
            }

            ClearRightHand();

            if (IsInLeftHand)
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
            if (IsInLeftHand)
            {
                _hands.UnsetItemLeftHand();

                if (IsEquippedToHolster(out var holsterItemView))
                    holsterItemView.PutAwayLeftHand(callback);
                else
                    _procedureScheduler.PutAwayLeftHand(callback);

                return;
            }

            if (IsInRightHand)
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
            if (Holsters.IsEquippedToHolster(out InventoryItemHolster holster))
            {
                holster.Unequip(callback);
                return;
            }

            callback?.Invoke();
        }

        public void ClearFromHands(Action callback = null)
        {
            if (IsInLeftHand)
            {
                _hands.UnsetItemLeftHand();

                if (IsEquippedToHolster(out var holster))
                    holster.PutAwayLeftHand(callback);
                else if (IsInStorage)
                    _procedureScheduler.PutAwayLeftHand(callback);
                else if (IsTemporary)
                    _procedureScheduler.DisappearLeftHand();
                else
                    _procedureScheduler.DropLeftHand();

                return;
            }

            if (IsInRightHand)
            {
                _hands.UnsetItemRightHand();

                if (IsEquippedToHolster(out var holster))
                    holster.PutAwayRightHand(callback);
                else if (IsInStorage)
                    _procedureScheduler.PutAwayRightHand(callback);
                else if (IsTemporary)
                    _procedureScheduler.DisappearRightHand();
                else
                    _procedureScheduler.DropRightHand();

                return;
            }

            callback?.Invoke();
        }

        public void Discard()
        {
            if (Holsters.IsEquippedToHolster(out InventoryItemHolster holster))
                holster.Discard();

            if (IsInLeftHand)
            {
                _hands.UnsetItemLeftHand();
                _procedureScheduler.DisappearLeftHand();
            }
            else if (IsInRightHand)
            {
                _hands.UnsetItemRightHand();
                _procedureScheduler.DisappearRightHand();
            }

            Storable.Do(x => x.Discard());
        }

        public void SwapHands()
        {
            if (IsInLeftHand)
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
            else if (IsInRightHand)
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

        internal void SetupHolsters(InventoryItemHolsters holsters) => Holsters = holsters;

        internal void SetupConsumable(IConsumableProcedures procedures, IEnumerable<Effect> effects) =>
            Consumable = new Consumable(this, effects, procedures, _procedureScheduler.Queue);

        internal void SetupStorable(StoredItems storage, int weight) =>
            Storable = new Storable(this, storage, weight);

        internal void DisappearFromHolster(Action callback = null)
        {
            if (Holsters.IsEquippedToHolster(out InventoryItemHolster holster))
            {
                holster.Disappear(callback);
                return;
            }

            callback?.Invoke();
        }

        internal void OnRemovedFromStorage()
        {
            if (!IsInHand && !Holsters.Any(x => x.IsEquipped))
                _procedureScheduler.SpawnAndDrop();
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

        bool IsEquippedToHolster(out ItemHolsterProcedureScheduler procedureScheduler) =>
            Holsters.IsEquippedToHolster(out procedureScheduler);

        public override string ToString()
        {
            return Name;
        }
    }
}