using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    public class InventoryMenuScript : MonoBehaviour
    {
        [SerializeField] ApparelSlotsMenuScript _apparelSlotsMenu;
        [SerializeField] HandsMenuScript _handsMenu;
        [SerializeField] HolstersMenuScript _holstersMenu;
    }
}