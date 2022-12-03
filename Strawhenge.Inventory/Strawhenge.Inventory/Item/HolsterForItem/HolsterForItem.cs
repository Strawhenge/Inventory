using Strawhenge.Inventory.Containers;
using System;

namespace Strawhenge.Inventory.Items.HolsterForItem
{
    public class HolsterForItem : IHolsterForItem
    {
        readonly IItem _item;
        readonly IHolster _holster;
        readonly IHolsterForItemView _view;

        public HolsterForItem(IItem item, IHolster holster, IHolsterForItemView view)
        {
            _item = item;
            _holster = holster;
            _view = view;

            view.Released += OnRemoved;
        }

        public string HolsterName => _holster.Name;

        public bool IsEquipped => _holster.IsCurrentItem(_item);

        public void Equip(Action callback = null)
        {
            if (IsEquipped)
            {
                callback?.Invoke();
                return;
            }

            ClearHolster();

            _item.UnequipFromHolster();
            _holster.SetItem(_item);

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

            _holster.UnsetItem();

            if (_item.IsInHand)
                callback?.Invoke();
            else
                _view.Hide(callback);
        }

        public IHolsterForItemView GetView()
        {
            return _view;
        }

        void ClearHolster()
        {
            _holster.CurrentItem.Do(
                x => x.UnequipFromHolster());
        }

        void OnRemoved()
        {
            if (_holster.IsCurrentItem(_item))
                _holster.UnsetItem();
        }
    }
}