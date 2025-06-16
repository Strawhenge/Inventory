using Strawhenge.Inventory.Loot;
using UnityEngine;

public class LootboxScript : MonoBehaviour
{
    public void OnStateChanged(ILootCollectionInfo info)
    {
        if (info.Count == 0)
            Destroy(gameObject);
    }
}