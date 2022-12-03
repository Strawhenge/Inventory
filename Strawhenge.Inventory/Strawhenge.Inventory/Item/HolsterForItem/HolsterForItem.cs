using Strawhenge.Inventory.Containers;
using System;

namespace Strawhenge.Inventory.Items.HolsterForItem
{
    public class HolsterForItem : IHolsterForItem
    {
        readonly IItem item;
        readonly IHolster holster;
        readonly IHolsterForItemView view;

        public HolsterForItem(IItem item, IHolster holster, IHolsterForItemView view)
        {
            this.item = item;
            this.holster = holster;
            this.view = view;

            view.Released += OnRemoved;
        }

        public string HolsterName => holster.Name;

        public bool IsEquipped => holster.IsCurrentItem(item);

        public void Equip(Action callback = null)
        {
            if (IsEquipped)
            {
                callback?.Invoke();
                return;
            }

            ClearHolster();

            item.UnequipFromHolster();
            holster.SetItem(item);

            if (item.IsInHand)
                callback?.Invoke();
            else
                view.Show(callback);

        }

        public void Unequip(Action callback = null)
        {
            if (!IsEquipped)
            {
                callback?.Invoke();
                return;
            }

            holster.UnsetItem();

            if (item.IsInHand)
                callback?.Invoke();
            else
                view.Hide(callback);
        }

        public IHolsterForItemView GetView()
        {
            return view;
        }

        void ClearHolster()
        {
            holster.CurrentItem.Do(
                x => x.UnequipFromHolster());
        }

        void OnRemoved()
        {
            if (holster.IsCurrentItem(item))
                holster.UnsetItem();
        }
    }
}
