using Strawhenge.Inventory.Containers;
using System.Collections.Generic;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Menu.Holsters
{
    [RequireComponent(typeof(RectTransform))]
    public class HolstersMenuScript : MonoBehaviour
    {
        [SerializeField] HolsterMenuEntryScript _menuEntryPrefab;
        [SerializeField] RectTransform _entryContainer;

        PanelContainer _container;

        void Awake()
        {
            var rectTransform = GetComponent<RectTransform>();
            _container = new PanelContainer(_entryContainer, rectTransform);
        }

        internal void Set(IEnumerable<ItemContainer> holsters)
        {
            foreach (var holster in holsters)
            {
                var menuEntry = Instantiate(_menuEntryPrefab);
                _container.Add(menuEntry.GetComponent<RectTransform>());
                menuEntry.Set(holster);
            }
        }
    }
}