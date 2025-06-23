using System;
using Strawhenge.Inventory.Apparel;

namespace Strawhenge.Inventory.Tests
{
    class ApparelViewFake : IApparelView
    {
        public ApparelViewFake(string itemName)
        {
            ItemName = itemName;
        }

        public event Action ShowInvoked;
        public event Action HideInvoked;
        public event Action DropInvoked;

        public string ItemName { get; }

        public void Show() => ShowInvoked?.Invoke();

        public void Hide() => HideInvoked?.Invoke();

        public void Drop() => DropInvoked?.Invoke();
    }
}