using System;

namespace Strawhenge.Inventory.Items
{
    public abstract class ClearFromHandsPreference
    {
        public static ClearFromHandsPreference Disappear => new PreferDisappear();

        public static ClearFromHandsPreference PutAway => new PreferPutAway();

        public static ClearFromHandsPreference Drop => new PreferDrop();

        public abstract void PerformClearLeftHand(IItemView itemView, Action callback = null);

        public abstract void PerformClearRightHand(IItemView itemView, Action callback = null);

        class PreferDisappear : ClearFromHandsPreference
        {
            public override void PerformClearLeftHand(IItemView itemView, Action callback = null) => itemView.Disappear(callback);

            public override void PerformClearRightHand(IItemView itemView, Action callback = null) => itemView.Disappear(callback);
        }

        class PreferPutAway : ClearFromHandsPreference
        {
            public override void PerformClearLeftHand(IItemView itemView, Action callback = null) => itemView.PutAwayLeftHand(callback);

            public override void PerformClearRightHand(IItemView itemView, Action callback = null) => itemView.PutAwayRightHand(callback);
        }

        class PreferDrop : ClearFromHandsPreference
        {
            public override void PerformClearLeftHand(IItemView itemView, Action callback = null) => itemView.DropLeftHand(callback);

            public override void PerformClearRightHand(IItemView itemView, Action callback = null) => itemView.DropRightHand(callback);
        }
    }
}