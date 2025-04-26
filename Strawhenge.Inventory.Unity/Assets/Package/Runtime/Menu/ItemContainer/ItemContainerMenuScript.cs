using Strawhenge.Common;
using Strawhenge.Common.Unity;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Items;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Strawhenge.Inventory.Unity
{
    public class ItemContainerMenuScript : MonoBehaviour
    {
        [SerializeField] Canvas _canvas;
        [SerializeField] RectTransform _entriesContainer;

        [FormerlySerializedAs("_itemMenuEntryPrefab")] [SerializeField]
        ContainedItemMenuEntryScript _containedItemMenuEntryPrefab;

        [SerializeField] ApparelPieceMenuEntryScript _apparelPieceMenuEntryPrefab;
        [SerializeField] EventScriptableObject[] _openEvents;
        [SerializeField] EventScriptableObject[] _closeEvents;
        [SerializeField] InventoryScript _inventoryScript;

        readonly List<GameObject> _menuEntries = new List<GameObject>();

        internal event System.Action Opened;
        internal event System.Action Closed;

        public ItemContainerMenuScriptContainer MenuContainer { private get; set; }

        public bool IsOpen { get; private set; }

        void Start()
        {
            _canvas.enabled = false;
            MenuContainer.Set(this);
        }

        public void Open(IItemContainerSource source)
        {
            if (IsOpen)
                Close();

            foreach (var item in source.GetItems())
                AddItem(item);

            foreach (var apparelPiece in source.GetApparelPieces())
                AddApparelPiece(apparelPiece);

            _canvas.enabled = true;
            _openEvents.ForEach(x => x.Invoke(gameObject));
            IsOpen = true;
            Opened?.Invoke();
        }

        public void Close()
        {
            if (!IsOpen) return;

            foreach (var gameObject in _menuEntries)
                Destroy(gameObject);

            _menuEntries.Clear();
            _canvas.enabled = false;
            _closeEvents.ForEach(x => x.Invoke(gameObject));
            IsOpen = false;
            Closed?.Invoke();
        }

        void AddItem(IContainedItem<ItemData> item)
        {
            var menuEntry = Instantiate(_containedItemMenuEntryPrefab, parent: _entriesContainer);
            menuEntry.Set(_inventoryScript.Inventory, item);
            _menuEntries.Add(menuEntry.gameObject);

            item.Removed += () =>
            {
                if (_menuEntries.Contains(menuEntry.gameObject))
                    _menuEntries.Remove(menuEntry.gameObject);
                Destroy(menuEntry.gameObject);
            };
        }

        void AddApparelPiece(IContainedItem<ApparelPieceData> apparelPiece)
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