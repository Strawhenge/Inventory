using Strawhenge.Inventory.Unity.Monobehaviours;
using System.Collections;
using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    public class InventoryMenuScript : MonoBehaviour
    {
        [SerializeField] Canvas _canvas;
        [SerializeField] ApparelSlotsMenuScript _apparelSlotsMenu;
        [SerializeField] HandsMenuScript _handsMenu;
        [SerializeField] HolstersMenuScript _holstersMenu;
        [SerializeField] InventoryScript _inventoryScript;

        void Start()
        {
            StartCoroutine(Setup());
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

        [ContextMenu(nameof(Show))]
        public void Show()
        {
            _canvas.enabled = true;
        }

        [ContextMenu(nameof(Hide))]
        public void Hide()
        {
            _canvas.enabled = false;
        }
    }
}