using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    public class LootInventoryScript : MonoBehaviour
    {
        public LootInventory LootInventory { private get; set; }

        public bool CanBeLooted(out IItemContainerSource source) => LootInventory.CanBeLooted(out source);
    }
}