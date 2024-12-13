using FunctionalUtilities;
using Strawhenge.Inventory.Unity.Items.Data;
using System;
using UnityEngine;

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

        public ItemScript Spawn() =>
            new GameObject(nameof(NullItemHelper)).AddComponent<ItemScript>();

        public Maybe<ItemPickupScript> Release() => Maybe.None<ItemPickupScript>();
    }
}