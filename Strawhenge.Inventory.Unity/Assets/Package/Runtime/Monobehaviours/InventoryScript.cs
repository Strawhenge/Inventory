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
        [FormerlySerializedAs("LeftHand"), SerializeField]
        Transform _leftHand;

        [FormerlySerializedAs("RightHand"), SerializeField]
        Transform _rightHand;

        [FormerlySerializedAs("itemsInHolsters"), SerializeField]
        ItemsInHolsters[] _itemsInHolsters;

        [FormerlySerializedAs("apparel"), SerializeField]
        ApparelPieceScriptableObject[] _apparel;

        public bool IsConfigurationComplete { get; private set; }

        public IInventory Inventory { get; set; }

        public ApparelSlotScripts ApparelSlots { private get; set; }

        public HandComponents HandComponents { private get; set; }

        public HolsterComponents HolsterComponents { private get; set; }

        public InventoryLoader Loader { private get; set; }

        public LoadInventoryDataGenerator LoadInventoryDataGenerator { private get; set; }

        public void Load(LoadInventoryData data) => Loader.Load(data);

        public LoadInventoryData GenerateCurrentLoadData() => LoadInventoryDataGenerator.GenerateCurrentLoadData();

        void Start()
        {
            if (_leftHand == null || _rightHand == null)
            {
                var animator = GetComponent<Animator>();

                if (_leftHand == null)
                {
                    Debug.Log($"Left hand Transform not set. Getting from animator.", this);
                    _leftHand = animator.GetBoneTransform(HumanBodyBones.LeftHand);
                }

                if (_rightHand == null)
                {
                    Debug.Log($"Right hand Transform not set. Getting from animator.", this);
                    _rightHand = animator.GetBoneTransform(HumanBodyBones.RightHand);
                }
            }

            if (_leftHand != null && _rightHand != null)
                HandComponents.Initialize(_leftHand, _rightHand);

            foreach (var holster in GetComponentsInChildren<HolsterScript>())
                HolsterComponents.Add(holster.HolsterName, holster.transform);

            foreach (var item in _itemsInHolsters.Where(x => x != null && x._item != null && x._holster != null))
            {
                Inventory
                    .CreateItem(item._item)
                    .Holsters
                    .FirstOrDefault(x => x.HolsterName.Equals(item._holster.Name))?
                    .Equip();
            }

            foreach (var apparelSlot in GetComponentsInChildren<ApparelSlotScript>())
                ApparelSlots.Add(apparelSlot);

            foreach (var apparelPiece in _apparel)
                Inventory.CreateApparelPiece(apparelPiece).Equip();

            IsConfigurationComplete = true;
        }

        [Serializable]
        class ItemsInHolsters
        {
            [FormerlySerializedAs("item")] public ItemScriptableObject _item;
            [FormerlySerializedAs("holster")] public HolsterScriptableObject _holster;
        }
    }
}