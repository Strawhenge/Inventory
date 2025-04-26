using Strawhenge.Inventory.Info;
using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Loader;
using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    public class InventoryScript : MonoBehaviour
    {
        [SerializeField] LeftHandScript _leftHand;
        [SerializeField] RightHandScript _rightHand;
        [SerializeField] int _maxStoredItemsWeight;
        [SerializeField] FixedItemContainerScript _apparelContainerPrefab;

        public bool IsConfigurationComplete { get; private set; }

        public Inventory Inventory { get; set; }

        public ApparelSlotScripts ApparelSlots { private get; set; }

        public HandScriptsContainer HandScriptsContainer { private get; set; }

        public HolsterScriptsContainer HolsterScriptsContainer { private get; set; }

        public ISetApparelContainerPrefab ApparelContainer { private get; set; }

        public InventoryLoader Loader { private get; set; }

        public InventoryInfoGenerator InfoGenerator { private get; set; }

        public void Load(LoadInventoryData data)
        {
            this.InvokeAsSoonAs(
                condition: () => IsConfigurationComplete, 
                action: () => Loader.Load(data));
        }

        public InventoryInfo GenerateCurrentInfo() => InfoGenerator.GenerateCurrentInfo();

        void Start()
        {
            if (_leftHand != null && _rightHand != null)
                HandScriptsContainer.Initialize(_leftHand, _rightHand);
            else
                Debug.LogError("Hand components not set.", this);

            foreach (var holster in GetComponentsInChildren<HolsterScript>())
            {
                HolsterScriptsContainer.Add(holster);
                Inventory.Holsters.Add(holster.HolsterName);
            }

            foreach (var apparelSlot in GetComponentsInChildren<ApparelSlotScript>())
            {
                ApparelSlots.Add(apparelSlot);
                Inventory.ApparelSlots.Add(apparelSlot.SlotName);
            }

            if (_apparelContainerPrefab != null)
                ApparelContainer.Set(_apparelContainerPrefab);

            Inventory.StoredItems.SetWeightCapacity(_maxStoredItemsWeight);

            IsConfigurationComplete = true;
        }
    }
}