using UnityEngine;

namespace Strawhenge.Inventory.Unity.Menu.SampleInventoryMenu
{
    public class HolstersMenuScript : MonoBehaviour
    {
        [SerializeField] ItemMenuScript _selectedItemMenu;
        [SerializeField] HolstersMenuEntryScript _menuEntryPrefab;
        [SerializeField] RectTransform _menuEntriesParent;

        internal void SetInventory(Inventory inventory)
        {
            foreach (var holster in inventory.Holsters)
            {
                var menuEntry = Instantiate(_menuEntryPrefab, _menuEntriesParent);
                menuEntry.SetHolster(holster);
                menuEntry.Selected += item => _selectedItemMenu.SetItem(item);
            }
        }
    }
}