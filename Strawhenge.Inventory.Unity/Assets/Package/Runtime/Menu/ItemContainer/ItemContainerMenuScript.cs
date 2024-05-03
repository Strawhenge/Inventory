using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Data;
using Strawhenge.Inventory.Unity.Monobehaviours;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    public class ItemContainerMenuScript : MonoBehaviour
    {
        [SerializeField] Canvas _canvas;
        [SerializeField] RectTransform _entriesContainer;
        [SerializeField] ItemMenuEntryScript _itemMenuEntryPrefab;
        [SerializeField] ApparelPieceMenuEntryScript _apparelPieceMenuEntryPrefab;
        [SerializeField] InventoryScript _inventoryScript;

        readonly List<GameObject> _menuEntries = new List<GameObject>();

        public ItemContainerMenuScriptContainer MenuContainer { private get; set; }

        void Start()
        {
            MenuContainer.Set(this);
        }

        public void Open(IItemContainerSource source)
        {
            foreach (var item in source.GetItems())
                AddItem(item);

            foreach (var apparelPiece in source.GetApparelPieces())
                AddApparelPiece(apparelPiece);

            _canvas.enabled = true;
        }

        public void Close()
        {
            foreach (var gameObject in _menuEntries)
                Destroy(gameObject);

            _menuEntries.Clear();
            _canvas.enabled = false;
        }

        void AddItem(IContainedItem<IItemData> item)
        {
            var menuEntry = Instantiate(_itemMenuEntryPrefab, parent: _entriesContainer);
            menuEntry.Set(_inventoryScript.Inventory, item);
            _menuEntries.Add(menuEntry.gameObject);

            item.Removed += () =>
            {
                if (_menuEntries.Contains(menuEntry.gameObject))
                    _menuEntries.Remove(menuEntry.gameObject);
                Destroy(menuEntry.gameObject);
            };
        }

        void AddApparelPiece(IContainedItem<IApparelPieceData> apparelPiece)
        {
            var menuEntry = Instantiate(_apparelPieceMenuEntryPrefab, parent: _entriesContainer);
            menuEntry.Set(_inventoryScript.Inventory, apparelPiece);
            _menuEntries.Add(menuEntry.gameObject);

            apparelPiece.Removed += () =>
            {
                if (_menuEntries.Contains(menuEntry.gameObject))
                    _menuEntries.Remove(menuEntry.gameObject);
                Destroy(menuEntry.gameObject);
            };
        }
    }
}