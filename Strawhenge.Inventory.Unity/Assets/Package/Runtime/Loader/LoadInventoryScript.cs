using Strawhenge.Common;
using Strawhenge.Common.Unity.Helpers;
using Strawhenge.Inventory.Loader;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Loader
{
    [RequireComponent(typeof(InventoryScript))]
    public class LoadInventoryScript : MonoBehaviour
    {
        [SerializeField] InventoryScript _inventory;
        [SerializeField] SerializedLoadInventoryItem[] _items;
        [SerializeField] SerializedLoadApparelPiece[] _apparel;

        void Awake()
        {
            ComponentRefHelper.EnsureHierarchyComponent(ref _inventory, nameof(_inventory), this);
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