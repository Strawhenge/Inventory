using Strawhenge.Inventory.Containers;
using System;
using System.Collections;
using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    [RequireComponent(typeof(RectTransform))]
    public class HolstersMenuScript : MonoBehaviour
    {
        [SerializeField] HolsterMenuEntryScript _menuEntryPrefab;
        [SerializeField] RectTransform _entryContainer;

        RectTransform _rectTransform;
        PanelContainer _container;

        void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _container = new PanelContainer(_entryContainer);
        }

        [ContextMenu("Add")]
        public void Add()
        {
            var menuEntry = Instantiate(_menuEntryPrefab);

            _container.Add(menuEntry.GetComponent<RectTransform>());

            Resize();
        }

        void Resize()
        {
            var entriesHeight = _container.EntriesHeight;

            _rectTransform.offsetMin =
                new Vector2(_rectTransform.offsetMin.x, _container.PositionY - entriesHeight);
        }
    }
}