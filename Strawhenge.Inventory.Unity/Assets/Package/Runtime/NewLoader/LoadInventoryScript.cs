using Strawhenge.Inventory.Unity.Monobehaviours;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.NewLoader
{
    [RequireComponent(typeof(InventoryScript))]
    public class LoadInventoryScript : MonoBehaviour
    {
        [SerializeField] SerializedLoadInventoryItem[] _items;

        InventoryScript _inventory;

        void Awake()
        {
            _inventory = GetComponent<InventoryScript>();
        }
    }
}