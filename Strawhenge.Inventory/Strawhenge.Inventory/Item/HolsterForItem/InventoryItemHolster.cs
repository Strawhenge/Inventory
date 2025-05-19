using Strawhenge.Inventory.Containers;
using System;
using Strawhenge.Inventory.Procedures;

namespace Strawhenge.Inventory.Items.Holsters
{
    public class InventoryItemHolster
    {
        readonly InventoryItem _item;
        readonly ItemContainer _itemContainer;
        readonly ItemHolsterProcedureScheduler _procedures;

        public InventoryItemHolster(
            InventoryItem item,
            ItemContainer itemContainer,
            IItemHolsterProcedures procedures,
            ProcedureQueue procedureQueue)
        {
            _item = item;
            _itemContainer = itemContainer;
            _procedures = new ItemHolsterProcedureScheduler(procedures, procedureQueue);
        }

        public string HolsterName => _itemContainer.Name;

        public bool IsEquipped => _itemContainer.IsCurrentItem(_item);

        public void Equip(Action callback = null)
        {
            if (IsEquipped)
            {
                callback?.Invoke();
                return;
            }

            ClearHolster();

            _item.DisappearFromHolster();
            _itemContainer.SetItem(_item);

            if (_item.IsInHand)
                callback?.Invoke();
            else
                _procedures.Show(callback);
        }

        public void Unequip(Action callback = null)
        {
            if (!IsEquipped)
            {
                callback?.Invoke();
                return;
            }

            _itemContainer.UnsetItem();

            if (_item.IsInHand)
            {
                callback?.Invoke();
                return;
            }

            if (_item.IsInStorage)
                _procedures.Hide(callback);
            else
                _procedures.Drop(callback);
        }

        internal void Disappear(Action callback = null)
        {
            if (!IsEquipped)
            {
                callback?.Invoke();
                return;
            }

            _itemContainer.UnsetItem();

            if (_item.IsInHand)
                callback?.Invoke();
            else
                _procedures.Hide();
        }

        internal void Drop(Action callback = null)
        {
            if (!IsEquipped)
            {
                callback?.Invoke();
                return;
            }

            _itemContainer.UnsetItem();

            if (_item.IsInHand)
                callback?.Invoke();
            else
                _procedures.Drop();
        }

        internal void Discard(Action callback = null)
        {
            if (!IsEquipped)
            {
                callback?.Invoke();
                return;
            }

            _itemContainer.UnsetItem();

            if (_item.IsInHand)
                callback?.Invoke();
            else
                _procedures.Hide();
        }

        internal ItemHolsterProcedureScheduler GetProcedureScheduler() => _procedures;

        void ClearHolster()
        {
            _itemContainer.CurrentItem.Do(
                x => x.UnequipFromHolster());
        }
    }
}