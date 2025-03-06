using System;
using Strawhenge.Inventory.Items.Holsters;

namespace Strawhenge.Inventory.Items
{
    public abstract class ClearFromHolsterPreference
    {
        public static ClearFromHolsterPreference Disappear => new PreferDisappear();

        public static ClearFromHolsterPreference Drop => new PreferDrop();

        public abstract void PerformClear(IHolsterForItemView holster, Action callback = null);

        class PreferDisappear : ClearFromHolsterPreference
        {
            public override void PerformClear(IHolsterForItemView holster, Action callback = null) =>
                holster.Hide(callback);
        }

        class PreferDrop : ClearFromHolsterPreference
        {
            public override void PerformClear(IHolsterForItemView holster, Action callback = null) =>
                holster.Drop(callback);
        }
    }
}