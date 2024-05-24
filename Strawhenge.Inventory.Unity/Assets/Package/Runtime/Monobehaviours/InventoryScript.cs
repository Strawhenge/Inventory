using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Data.ScriptableObjects;
using Strawhenge.Inventory.Unity.Loader;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Strawhenge.Inventory.Unity.Monobehaviours
{
    public class InventoryScript : MonoBehaviour
    {
        [SerializeField] Transform _leftHand;
        [SerializeField] Transform _rightHand;
        [SerializeField] FixedItemContainerScript _apparelContainerPrefab;

        public bool IsConfigurationComplete { get; private set; }

        public IInventory Inventory { get; set; }

        public ApparelSlotScripts ApparelSlots { private get; set; }

        public HandComponents HandComponents { private get; set; }

        public HolsterComponents HolsterComponents { private get; set; }

        public ISetApparelContainerPrefab ApparelContainer { private get; set; }

        public InventoryLoader Loader { private get; set; }

        public LoadInventoryDataGenerator LoadInventoryDataGenerator { private get; set; }

        public void Load(LoadInventoryData data) => Loader.Load(data);

        public LoadInventoryData GenerateCurrentLoadData() => LoadInventoryDataGenerator.GenerateCurrentLoadData();

        void Start()
        {
            if (_leftHand != null && _rightHand != null)
                HandComponents.Initialize(_leftHand, _rightHand);
            else
                Debug.LogError("Hand components not set.", this);

            foreach (var holster in GetComponentsInChildren<HolsterScript>())
                HolsterComponents.Add(holster.HolsterName, holster.transform);

            foreach (var apparelSlot in GetComponentsInChildren<ApparelSlotScript>())
                ApparelSlots.Add(apparelSlot);

            if (_apparelContainerPrefab != null)
                ApparelContainer.Set(_apparelContainerPrefab);
            else
                Debug.LogError("Apparel container prefab not set.", this);

            IsConfigurationComplete = true;
        }
    }
}