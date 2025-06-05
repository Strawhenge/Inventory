using Strawhenge.Common;
using Strawhenge.Common.Unity;
using Strawhenge.Common.Unity.Helpers;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Items;
using System.Collections.Generic;
using UnityEngine;
using Strawhenge.Inventory.Loot;
using UnityEngine.Events;

namespace Strawhenge.Inventory.Unity.Menu
{
    public class LootMenuScript : MonoBehaviour
    {
        [SerializeField] InventoryScript _inventoryScript;
        [SerializeField] TakeItemLootMenuScript _takeItemLootMenu;
        [SerializeField] EventScriptableObject[] _openEvents;
        [SerializeField] UnityEvent _opened;
        [SerializeField] EventScriptableObject[] _closeEvents;
        [SerializeField] UnityEvent _closed;

        [SerializeField] RectTransform _containerPanel;
        [SerializeField] RectTransform _entriesContainer;
        [SerializeField] ItemLootMenuEntryScript _itemLootMenuEntryPrefab;
        [SerializeField] ApparelPieceLootMenuEntryScript _apparelPieceLootMenuEntryPrefab;

        readonly List<GameObject> _menuEntries = new();

        public bool IsOpen { get; private set; }

        void Awake()
        {
            _containerPanel.gameObject.SetActive(false);

            ComponentRefHelper.EnsureSceneComponent(ref _takeItemLootMenu, nameof(_takeItemLootMenu), this);
        }

        public void Open(ILootSource source)
        {
            if (IsOpen)
                Close();
            IsOpen = true;

            foreach (var item in source.GetItems())
                AddItem(item);

            foreach (var apparelPiece in source.GetApparelPieces())
                AddApparelPiece(apparelPiece);

            _containerPanel.gameObject.SetActive(true);
            _openEvents.ForEach(x => x.Invoke(gameObject));
            _opened.Invoke();
        }

        public void Close()
        {
            if (!IsOpen) return;
            IsOpen = false;

            foreach (var gameObject in _menuEntries)
                Destroy(gameObject);

            _takeItemLootMenu.Hide();

            _menuEntries.Clear();
            _containerPanel.gameObject.SetActive(false);
            _closeEvents.ForEach(x => x.Invoke(gameObject));
            _closed.Invoke();
        }

        void AddItem(Loot<Item> item)
        {
            var menuEntry = Instantiate(_itemLootMenuEntryPrefab, parent: _entriesContainer);
            menuEntry.Set(_inventoryScript.Inventory, item, _takeItemLootMenu);
            _menuEntries.Add(menuEntry.gameObject);

            item.Taken += () =>
            {
                if (_menuEntries.Contains(menuEntry.gameObject))
                    _menuEntries.Remove(menuEntry.gameObject);
                Destroy(menuEntry.gameObject);
            };
        }

        void AddApparelPiece(Loot<ApparelPiece> apparelPiece)
        {
            var menuEntry = Instantiate(_apparelPieceLootMenuEntryPrefab, parent: _entriesContainer);
            menuEntry.Set(_inventoryScript.Inventory, apparelPiece);
            _menuEntries.Add(menuEntry.gameObject);

            apparelPiece.Taken += () =>
            {
                if (_menuEntries.Contains(menuEntry.gameObject))
                    _menuEntries.Remove(menuEntry.gameObject);
                Destroy(menuEntry.gameObject);
            };
        }
    }
}