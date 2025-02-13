using System;
using System.Reflection;
using Strawhenge.Inventory.Items.HolsterForItem;

namespace Strawhenge.Inventory.Tests
{
    class HolsterForItemViewFake : IHolsterForItemView
    {
        public HolsterForItemViewFake(string itemName, string holsterName)
        {
            ItemName = itemName;
            HolsterName = holsterName;
        }

        public event Action<HolsterForItemViewFake, MethodInfo> MethodInvoked;

        public event Action Released;

        public string ItemName { get; }

        public string HolsterName { get; }

        public void DrawLeftHand(Action callback = null) =>
            HandleInvocation(nameof(DrawLeftHand), callback);

        public void DrawRightHand(Action callback = null) =>
            HandleInvocation(nameof(DrawRightHand), callback);

        public void PutAwayLeftHand(Action callback = null) =>
            HandleInvocation(nameof(PutAwayLeftHand), callback);

        public void PutAwayRightHand(Action callback = null) =>
            HandleInvocation(nameof(PutAwayRightHand), callback);

        public void Show(Action callback = null) =>
            HandleInvocation(nameof(Show), callback);

        public void Hide(Action callback = null) =>
            HandleInvocation(nameof(Hide), callback);

        public void Drop(Action callback = null) =>
            HandleInvocation(nameof(Drop), callback);

        void HandleInvocation(string methodName, Action callback)
        {
            MethodInvoked?.Invoke(this, typeof(HolsterForItemViewFake).GetMethod(methodName));
            callback?.Invoke();
        }
    }
}