using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    public class ApparelSlotsMenuScript : MonoBehaviour
    {
        [SerializeField] ApparelSlotMenuEntryScript _menuEntryPrefab;
        [SerializeField] RectTransform _entryContainer;

        PanelContainer _container;

        void Awake()
        {
            var rectTransform = GetComponent<RectTransform>();
            _container = new PanelContainer(_entryContainer, rectTransform);
        }

        [ContextMenu("Add")]
        public void Add()
        {
            var menuEntry = Instantiate(_menuEntryPrefab);

            _container.Add(menuEntry.GetComponent<RectTransform>());
        }
    }
}
