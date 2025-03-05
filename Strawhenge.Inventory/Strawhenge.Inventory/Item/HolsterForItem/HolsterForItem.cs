using Strawhenge.Inventory.Containers;
using System;

namespace Strawhenge.Inventory.Items.HolsterForItem
{
    public class HolsterForItem : IHolsterForItem
    {
        readonly Item _item;
        readonly ItemContainer _itemContainer;
        readonly IHolsterForItemView _view;

        public HolsterForItem(Item item, ItemContainer itemContainer, IHolsterForItemView view)
        {
            _item = item;
            _itemContainer = itemContainer;
            _view = view;

            view.Released += OnRemoved;
        }

        public string HolsterName => _itemContainer.Name;

        public bool IsEquipped => _itemContainer.IsCurrentItem(_item);

        public ClearFromHolsterPreference ClearFromHolsterPreference { private get; set; } =
            ClearFromHolsterPreference.Disappear;

        public void Equip(Action callback = null)
        {
            if (IsEquipped)
            {
                callback?.Invoke();
                return;
            }

            ClearHolster();

            _item.UnequipFromHolster();
            _itemContainer.SetItem(_item);

            if (_item.IsInHand)
                callback?.Invoke();
            else
                _view.Show(callback);
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
                callback?.Invoke();
            else
                ClearFromHolsterPreference.PerformClear(_view, callback);
        }

        public void Discard(Action callback = null)
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
                _view.Hide();
        }

        public void Drop(Action callback = null)
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
                _view.Drop();
        }

        public IHolsterForItemView GetView()
        {
            return _view;
        }

        void ClearHolster()
        {
            _itemContainer.CurrentItem.Do(
                x => x.UnequipFromHolster());
        }

        void OnRemoved()
        {
            if (_itemContainer.IsCurrentItem(_item))
                _itemContainer.UnsetItem();
        }
    }
}