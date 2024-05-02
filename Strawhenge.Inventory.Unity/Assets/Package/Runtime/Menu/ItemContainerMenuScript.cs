using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Monobehaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    public class ItemContainerMenuScript : MonoBehaviour
    {
        [SerializeField] Canvas _canvas;
        [SerializeField] RectTransform _entriesContainer;
        [SerializeField] ApparelPieceMenuEntryScript _apparelPieceMenuEntryPrefab;
        [SerializeField] InventoryScript _inventoryScript;

        readonly List<GameObject> _menuEntries = new List<GameObject>();

        public void Open(IItemContainerSource source)
        {
            foreach (var apparelPiece in source.ApparelPieces)
            {
                var menuEntry = Instantiate(_apparelPieceMenuEntryPrefab, parent: _entriesContainer);
                menuEntry.Set(_inventoryScript.Inventory, apparelPiece);
                _menuEntries.Add(menuEntry.gameObject);
            }

            _canvas.enabled = true;
        }

        public void Close()
        {
            foreach (var gameObject in _menuEntries)
                Destroy(gameObject);

            _menuEntries.Clear();
            _canvas.enabled = false;
        }
    }

    public interface IItemContainerSource
    {
        IReadOnlyList<IContainedItem<IApparelPieceData>> ApparelPieces { get; }
    }

    public interface IContainedItem<T>
    {
        T Item { get; }

        void RemoveFromContainer();
    }
}