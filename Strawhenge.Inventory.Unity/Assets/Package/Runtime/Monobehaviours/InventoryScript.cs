using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Data.ScriptableObjects;
using System;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Monobehaviours
{
    public class InventoryScript : MonoBehaviour
    {
        public Transform LeftHand;
        public Transform RightHand;

        [SerializeField] ItemsInHolsters[] itemsInHolsters;
        [SerializeField] ApparelPieceScriptableObject[] apparel;

        public IItemManager ItemManager { get; set; }

        public ApparelManager ApparelManager { get; set; }

        public HandComponents HandComponents { private get; set; }

        public HolsterComponents HolsterComponents { private get; set; }

        private void Start()
        {
            if (LeftHand == null || RightHand == null)
            {
                var animator = GetComponent<Animator>();

                if (LeftHand == null)
                {
                    Debug.Log($"Left hand Transform not set. Getting from animator.", this);
                    LeftHand = animator.GetBoneTransform(HumanBodyBones.LeftHand);
                }

                if (RightHand == null)
                {
                    Debug.Log($"Right hand Transform not set. Getting from animator.", this);
                    RightHand = animator.GetBoneTransform(HumanBodyBones.RightHand);
                }
            }

            if (LeftHand != null && RightHand != null)
                HandComponents.Initialize(LeftHand, RightHand);

            foreach (var holster in GetComponentsInChildren<HolsterScript>())
                HolsterComponents.Add(holster.HolsterName, holster.transform);

            foreach (var item in itemsInHolsters.Where(x => x != null && x.item != null && x.holster != null))
            {
                ItemManager
                    .Manage(item.item)
                    .Holsters
                    .FirstOrDefault(x => x.HolsterName.Equals(item.holster.Name))?
                    .Equip();
            }

            foreach (var apparelSlot in GetComponentsInChildren<ApparelSlotScript>())
                ApparelManager.AddSlot(apparelSlot);

            foreach (var apparelPiece in apparel)
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
