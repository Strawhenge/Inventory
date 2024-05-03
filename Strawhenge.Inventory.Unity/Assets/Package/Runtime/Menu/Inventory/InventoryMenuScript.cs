using Strawhenge.Common;
using Strawhenge.Common.Unity;
using Strawhenge.Inventory.Unity.Menu.Apparel;
using Strawhenge.Inventory.Unity.Menu.Hands;
using Strawhenge.Inventory.Unity.Menu.Holsters;
using Strawhenge.Inventory.Unity.Monobehaviours;
using System.Collections;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Menu
{
    public class InventoryMenuScript : MonoBehaviour
    {
        [SerializeField] Canvas _canvas;
        [SerializeField] ApparelSlotsMenuScript _apparelSlotsMenu;
        [SerializeField] HandsMenuScript _handsMenu;
        [SerializeField] HolstersMenuScript _holstersMenu;
        [SerializeField] EventScriptableObject[] _openEvents;
        [SerializeField] EventScriptableObject[] _closeEvents;
        [SerializeField] InventoryScript _inventoryScript;

        public InventoryMenuScriptContainer Container { private get; set; }

        void Start()
        {
            Container.Set(menu: this);
            StartCoroutine(Setup());
        }

        void OnDestroy()
        {
            Container.Clear();
        }

        IEnumerator Setup()
        {
            yield return new WaitUntil(() => _inventoryScript.IsConfigurationComplete);

            // Sub menus currently must be configured whilst the canvas is enabled, otherwise they are positioned wrong.
            // Remove this wait when a solution is found.
            yield return new WaitUntil(() => _canvas.enabled);

            _apparelSlotsMenu.Set(_inventoryScript.Inventory.ApparelSlots);
            _holstersMenu.Set(_inventoryScript.Inventory.Holsters);
            _handsMenu.Set(_inventoryScript.Inventory.LeftHand, _inventoryScript.Inventory.RightHand);
        }

        [ContextMenu(nameof(Open))]
        public void Open()
        {
            _openEvents.ForEach(x => x.Invoke(gameObject));
            _canvas.enabled = true;
        }

        [ContextMenu(nameof(Close))]
        public void Close()
        {
            _canvas.enabled = false;
            _closeEvents.ForEach(x => x.Invoke(gameObject));
        }
    }
}