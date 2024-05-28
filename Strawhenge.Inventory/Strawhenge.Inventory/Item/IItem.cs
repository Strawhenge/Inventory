using System;
using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Items.Consumables;
using Strawhenge.Inventory.Items.HolsterForItem;

namespace Strawhenge.Inventory
{
    public interface IItem
    {
        event Action<IItem> Discarded;

        string Name { get; }

        bool IsInHand { get; }

        bool IsTwoHanded { get; }

        IEnumerable<IEquipItemToHolster> Holsters { get; }
        
        Maybe<IConsumable> Consumable { get; }

        ClearFromHandsPreference ClearFromHandsPreference { set; }

        ClearFromHolsterPreference ClearFromHolsterPreference { set; }

        void HoldLeftHand(Action callback = null);

        void HoldRightHand(Action callback = null);

        void PutAway(Action callback = null);

        void Drop(Action callback = null);

        void ClearFromHands(Action callback = null);

        void UnequipFromHolster(Action callback = null);

        void Discard();
    }
}