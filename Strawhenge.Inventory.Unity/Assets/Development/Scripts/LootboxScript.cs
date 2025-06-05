using Strawhenge.Common.Unity.Helpers;
using Strawhenge.Inventory.Loot;
using Strawhenge.Inventory.Unity.Loot;
using Strawhenge.Inventory.Unity.Menu;
using UnityEngine;

public class LootboxScript : MonoBehaviour
{
    public void OnStateChanged(ILootCollectionInfo info)
    {
        if (info.Count == 0)
            Destroy(gameObject);
    }
}