using System;
using System.Reflection;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory.Tests
{
    class ItemViewFake : IItemView
    {
        public ItemViewFake(string itemName)
        {
            ItemName = itemName;
        }

        public string ItemName { get; }

        public event Action<ItemViewFake, MethodInfo> MethodInvoked;

        public event Action Released;

        public void DrawLeftHand(Action callback = null) =>
            HandleInvocation(nameof(DrawLeftHand), callback);

        public void DrawRightHand(Action callback = null) =>
            HandleInvocation(nameof(DrawRightHand), callback);

        public void PutAwayLeftHand(Action callback = null) =>
            HandleInvocation(nameof(PutAwayLeftHand), callback);

        public void PutAwayRightHand(Action callback = null) =>
            HandleInvocation(nameof(PutAwayRightHand), callback);

        public void DropLeftHand(Action callback = null) =>
            HandleInvocation(nameof(DropLeftHand), callback);

        public void DropRightHand(Action callback = null) =>
            HandleInvocation(nameof(DropRightHand), callback);

        public void SpawnAndDrop(Action callback = null) =>
            HandleInvocation(nameof(SpawnAndDrop), callback);

        public void LeftHandToRightHand(Action callback = null) =>
            HandleInvocation(nameof(LeftHandToRightHand), callback);

        public void RightHandToLeftHand(Action callback = null) =>
            HandleInvocation(nameof(RightHandToLeftHand), callback);

        public void Disappear(Action callback = null) =>
            HandleInvocation(nameof(Disappear), callback);

        void HandleInvocation(string methodName, Action callback)
        {
            MethodInvoked?.Invoke(this, typeof(ItemViewFake).GetMethod(methodName));
            callback?.Invoke();
        }
    }
}