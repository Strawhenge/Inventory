using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity.NewMenu
{
    public class ApparelSlotsMenuScript : MonoBehaviour
    {
        [SerializeField] ApparelSlotsMenuEntryScript _menuEntryPrefab;
        [SerializeField] RectTransform _menuEntriesParent;

        public void SetInventory(Inventory inventory)
        {
            foreach (var slot in inventory.ApparelSlots)
            {
                var menuEntry = Instantiate(_menuEntryPrefab, _menuEntriesParent);
                menuEntry.SetApparelSlot(slot);
            }
        }
    }
}