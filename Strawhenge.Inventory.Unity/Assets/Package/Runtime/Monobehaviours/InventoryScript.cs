using Strawhenge.Inventory.Info;
using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Loader;
using System.Collections;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Monobehaviours
{
    public class InventoryScript : MonoBehaviour
    {
        [SerializeField] Transform _leftHand;
        [SerializeField] Transform _rightHand;
        [SerializeField] int _maxStoredItemsWeight;
        [SerializeField] FixedItemContainerScript _apparelContainerPrefab;

        public bool IsConfigurationComplete { get; private set; }

        public IInventory Inventory { get; set; }

        public IStoredItemsWeightCapacitySetter StoredItemsWeightCapacity { private get; set; }

        public ApparelSlotScripts ApparelSlots { private get; set; }

        public HandComponents HandComponents { private get; set; }

        public HolsterComponents HolsterComponents { private get; set; }

        public ISetApparelContainerPrefab ApparelContainer { private get; set; }

        public InventoryLoader Loader { private get; set; }

        public InventoryInfoGenerator InfoGenerator { private get; set; }

        public void Load(LoadInventoryData data)
        {
            if (IsConfigurationComplete)
                Loader.Load(data);
            else
                StartCoroutine(LoadCoroutine());

            IEnumerator LoadCoroutine()
            {
                yield return new WaitUntil(() => IsConfigurationComplete);
                Loader.Load(data);
            }
        }

        public InventoryInfo GenerateCurrentInfo() => InfoGenerator.GenerateCurrentInfo();

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

            StoredItemsWeightCapacity.SetWeightCapacity(_maxStoredItemsWeight);

            IsConfigurationComplete = true;
        }
    }
}