﻿using System;

namespace Strawhenge.Inventory.Items.HolsterForItem
{
    public interface IHolsterForItem : IEquipItemToHolster
    {
        ClearFromHolsterPreference ClearFromHolsterPreference { set; }

        void Discard(Action callback = null);

        IHolsterForItemView GetView();
    }
}