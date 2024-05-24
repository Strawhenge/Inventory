using Strawhenge.Inventory.Unity.Monobehaviours;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Loader
{
    [RequireComponent(typeof(InventoryScript))]
    public class LoadInventoryScript : MonoBehaviour
    {
        [SerializeField] SerializedLoadInventoryItem[] _items;
        [SerializeField] SerializedLoadApparelPiece[] _apparel;

        InventoryScript _inventory;

        void Awake()
        {
            _inventory = GetComponent<InventoryScript>();
        }

        void Start()
        {
            _inventory.Load(GetLoadData());
        }

        LoadInventoryData GetLoadData()
        {
            return new LoadInventoryData(
                _items.Where(x => x != null && x.ItemData != null),
                _apparel.Where(x => x != null && x.ApparelPiece != null));
        }
    }
}