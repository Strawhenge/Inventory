using Strawhenge.Inventory.Unity.Data;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Monobehaviours;
using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    public class Spawner : ISpawner
    {
        public void Despawn(ItemScript script) => Object.Destroy(script.gameObject);

        public ItemScript Spawn(IItemData item) => Object.Instantiate(item.Prefab);
    }
}
