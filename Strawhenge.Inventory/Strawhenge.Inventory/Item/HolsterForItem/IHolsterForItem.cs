using System;

namespace Strawhenge.Inventory.Items.HolsterForItem
{
    public interface IHolsterForItem
    {
        string HolsterName { get; }

        bool IsEquipped { get; }

        ClearFromHolsterPreference ClearFromHolsterPreference { set; }

        void Equip(Action callback = null);

        void Unequip(Action callback = null);

        void Discard(Action callback = null);

        IHolsterForItemView GetView();
    }
}