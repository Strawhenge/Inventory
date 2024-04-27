using Strawhenge.Inventory.Containers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    public class HolstersMenuScript : MonoBehaviour
    {
        [SerializeField] HolsterMenuEntryScript _menuEntryPrefab;
        [SerializeField] RectTransform _entryContainer;

        readonly List<RectTransform> _entries = new List<RectTransform>();

        [ContextMenu("Add")]
        public void Add()
        {
            Add(new Holster("Holster " + (_entries.Count + 1)));
        }

        void Add(IHolster holster)
        {
            var menuEntry = Instantiate(_menuEntryPrefab, _entryContainer);

            SetPosition(menuEntry.gameObject);

            menuEntry.SetHolster(holster);
        }

        void SetPosition(GameObject menuEntry)
        {
            var rectTransform = menuEntry.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = Vector2.zero;
            var height = rectTransform.rect.height;

            var y = _entries.Sum(x => -x.rect.height);

            rectTransform.offsetMin = new Vector2(0, y - height);
            rectTransform.offsetMax = new Vector2(0, y);

            _entries.Add(rectTransform);
        }
    }
}