using System;

namespace Strawhenge.Inventory.Items.HolsterForItem
{
    public interface IHolsterForItem : IEquipItemToHolster
    {
        ClearFromHolsterPreference ClearFromHolsterPreference { set; }

        void Discard(Action callback = null);

        IHolsterForItemView GetView();
    }

    public interface IEquipItemToHolster
    {
        string HolsterName { get; }

        bool IsEquipped { get; }

        void Equip(Action callback = null);

        void Unequip(Action callback = null);
    }
}