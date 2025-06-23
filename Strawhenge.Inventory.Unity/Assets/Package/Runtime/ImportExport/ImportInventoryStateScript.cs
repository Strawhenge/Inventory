using Strawhenge.Common;
using Strawhenge.Common.Unity.Helpers;
using Strawhenge.Inventory.ImportExport;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.ImportExport
{
    [RequireComponent(typeof(InventoryScript))]
    public class ImportInventoryStateScript : MonoBehaviour
    {
        [SerializeField] InventoryScript _inventory;
        [SerializeField] SerializedItemState[] _items;
        [SerializeField] SerializedApparelPieceState[] _apparel;

        void Awake()
        {
            ComponentRefHelper.EnsureHierarchyComponent(ref _inventory, nameof(_inventory), this);
        }

        void Start()
        {
            _inventory.Inventory.ImportState(new InventoryState(
                GetItems(),
                GetApparelPieces()));
        }

        IEnumerable<ItemState> GetItems()
        {
            return _items
                .ExcludeNull()
                .Select(x => x.Map());
        }

        IEnumerable<ApparelPieceState> GetApparelPieces()
        {
            return _apparel
                .ExcludeNull()
                .Select(x => x.Map());
        }
    }
}