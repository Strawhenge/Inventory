using Strawhenge.Common;
using Strawhenge.Inventory.Loader;
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
            var loadItems = _items
                .ExcludeNull()
                .Select(x => x.Map());

            var loadApparelPieces = _apparel
                .ExcludeNull()
                .Select(x => x.Map());

            return new LoadInventoryData(loadItems, loadApparelPieces);
        }
    }
}