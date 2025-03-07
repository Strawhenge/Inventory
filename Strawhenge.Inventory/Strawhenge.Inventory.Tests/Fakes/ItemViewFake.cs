using System;
using System.Reflection;
using Strawhenge.Inventory.Items;

#pragma warning disable CS0067 // Event is never used

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

        public void AppearLeftHand(Action callback = null) =>
            HandleInvocation(nameof(AppearLeftHand), callback);

        public void AppearRightHand(Action callback = null) =>
            HandleInvocation(nameof(AppearRightHand), callback);

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

        public void DisappearLeftHand(Action callback = null) =>
            HandleInvocation(nameof(DisappearLeftHand), callback);

        public void DisappearRightHand(Action callback = null) =>
            HandleInvocation(nameof(DisappearRightHand), callback);

        void HandleInvocation(string methodName, Action callback)
        {
            MethodInvoked?.Invoke(this, typeof(ItemViewFake).GetMethod(methodName));
            callback?.Invoke();
        }
    }
}