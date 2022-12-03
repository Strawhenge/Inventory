using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Data.ScriptableObjects;
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

        public IItemManager ItemManager { get; set; }

        public ApparelManager ApparelManager { get; set; }

        public HandComponents HandComponents { private get; set; }

        public HolsterComponents HolsterComponents { private get; set; }

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

            foreach (var item in _itemsInHolsters.Where(x => x != null && x.item != null && x.holster != null))
            {
                ItemManager
                    .Manage(item.item)
                    .Holsters
                    .FirstOrDefault(x => x.HolsterName.Equals(item.holster.Name))?
                    .Equip();
            }

            foreach (var apparelSlot in GetComponentsInChildren<ApparelSlotScript>())
                ApparelManager.AddSlot(apparelSlot);

            foreach (var apparelPiece in _apparel)
                ApparelManager.Create(apparelPiece).Equip();
        }

        [Serializable]
        class ItemsInHolsters
        {
            public ItemScriptableObject item;
            public HolsterScriptableObject holster;
        }
    }
}