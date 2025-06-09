using UnityEngine;

namespace Strawhenge.Inventory.Unity.Menu.SampleInventoryMenu
{
    public class ApparelSlotsMenuScript : MonoBehaviour
    {
        [SerializeField] ApparelSlotsMenuEntryScript _menuEntryPrefab;
        [SerializeField] RectTransform _menuEntriesParent;

        internal void SetInventory(Inventory inventory)
        {
            foreach (var slot in inventory.ApparelSlots)
            {
                var menuEntry = Instantiate(_menuEntryPrefab, _menuEntriesParent);
                menuEntry.SetApparelSlot(slot);
            }
        }
    }
}