using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    class PanelContainer
    {
        readonly List<RectTransform> _entries = new List<RectTransform>();
        readonly RectTransform _container;

        public PanelContainer(RectTransform container)
        {
            _container = container;
        }

        public float EntriesHeight => _entries.Sum(x => x.rect.height);

        public float PositionY => _container.offsetMax.y;

        public void Add(RectTransform rectTransform)
        {
            rectTransform.parent = _container;
            rectTransform.anchoredPosition = Vector2.zero;
            var height = rectTransform.rect.height;

            var entriesHeight = EntriesHeight;

            rectTransform.offsetMin = new Vector2(0, -entriesHeight - height);
            rectTransform.offsetMax = new Vector2(0, -entriesHeight);

            _entries.Add(rectTransform);
        }
    }
}