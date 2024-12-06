using Strawhenge.Inventory.Unity.Data;
using System;
using FunctionalUtilities;

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

        public Maybe<ItemPickupScript> Release() => Maybe.None<ItemPickupScript>();

        public ItemScript Spawn() => null;
    }
}
