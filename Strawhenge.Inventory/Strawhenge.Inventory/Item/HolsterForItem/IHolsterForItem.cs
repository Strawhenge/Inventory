using System;

namespace Strawhenge.Inventory.Items.HolsterForItem
{
    public interface IHolsterForItem
    {
        string HolsterName { get; }

        bool IsEquipped { get; }

        void Equip(Action callback = null);

        void Unequip(Action callback = null);

        IHolsterForItemView GetView();
    }
}
