using Strawhenge.Inventory.Unity.Data;
using System;

namespace Strawhenge.Inventory.Unity.Items
{
    public class NullItemHelper : IItemHelper
    {
        public IItemData Data { get; } = new NullItemData();

#pragma warning disable 67
        public event Action Released;
#pragma warning restore 67

        public void Despawn()
        {
        }

        public void Release()
        {
        }

        public ItemScript Spawn() => null;
    }
}