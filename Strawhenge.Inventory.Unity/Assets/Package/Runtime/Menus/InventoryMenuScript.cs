using Strawhenge.Inventory.Unity.Monobehaviours;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Strawhenge.Inventory.Unity
{
    public class InventoryMenuScript : MonoBehaviour
    {
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

            _apparelSlotsMenu.Set(_inventoryScript.Inventory.ApparelSlots);

        }
    }
}