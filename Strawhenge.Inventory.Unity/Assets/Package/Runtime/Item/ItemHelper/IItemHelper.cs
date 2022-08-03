using Strawhenge.Inventory.Unity.Data;
using Strawhenge.Inventory.Unity.Monobehaviours;
using System;

namespace Strawhenge.Inventory.Unity.Items
{
    public interface IItemHelper
    {
        event Action Released;

        IItemData Data { get; }

        void Despawn();

        ItemScript Spawn();

        Maybe<ItemScript> Release();
    }
}
