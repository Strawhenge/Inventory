using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    class PanelContainer
    {
        readonly List<RectTransform> _entries = new List<RectTransform>();
        readonly RectTransform _container;
        readonly RectTransform _parent;

        public PanelContainer(RectTransform container, RectTransform parent)
        {
            _container = container;
            _parent = parent;
        }

        public void Add(RectTransform rectTransform)
        {
            rectTransform.SetParent(_container);
            rectTransform.anchoredPosition = Vector2.zero;
            var height = rectTransform.rect.height;

            var entriesHeight = GetEntriesHeight();

            rectTransform.offsetMin = new Vector2(0, -entriesHeight - height);
            rectTransform.offsetMax = new Vector2(0, -entriesHeight);

            _entries.Add(rectTransform);

            ResizeParent();
        }

        public void Clear()
        {
            foreach (var entry in _entries)
                Object.Destroy(entry.gameObject);

            _entries.Clear();
            ResizeParent();
        }

        void ResizeParent()
        {
            var entriesHeight = GetEntriesHeight();

            _parent.offsetMin =
                new Vector2(_parent.offsetMin.x, _parent.offsetMax.y + _container.offsetMax.y - entriesHeight);
        }

        float GetEntriesHeight() => _entries.Sum(x => x.rect.height);
    }
}