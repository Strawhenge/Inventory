using Strawhenge.Inventory.Containers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    [RequireComponent(typeof(RectTransform))]
    public class HolstersMenuScript : MonoBehaviour
    {
        [SerializeField] HolsterMenuEntryScript _menuEntryPrefab;
        [SerializeField] RectTransform _entryContainer;

        readonly List<RectTransform> _entries = new List<RectTransform>();

        RectTransform _rectTransform;

        void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        [ContextMenu("Add")]
        public void Add()
        {
            Add(new Holster("Holster " + (_entries.Count + 1)));

            Resize();
        }

        void Add(IHolster holster)
        {
            var menuEntry = Instantiate(_menuEntryPrefab, _entryContainer);
            menuEntry.SetHolster(holster);

            AddMenuEntry(menuEntry.gameObject);
        }

        void AddMenuEntry(GameObject menuEntry)
        {
            var rectTransform = menuEntry.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = Vector2.zero;
            var height = rectTransform.rect.height;

            var y = _entries.Sum(x => -x.rect.height);

            rectTransform.offsetMin = new Vector2(0, y - height);
            rectTransform.offsetMax = new Vector2(0, y);

            _entries.Add(rectTransform);
        }

        void Resize()
        {
            var entriesHeight = _entries.Sum(x => x.rect.height);

            _rectTransform.offsetMin =
                new Vector2(_rectTransform.offsetMin.x, _entryContainer.offsetMax.y - entriesHeight);
        }
    }
}