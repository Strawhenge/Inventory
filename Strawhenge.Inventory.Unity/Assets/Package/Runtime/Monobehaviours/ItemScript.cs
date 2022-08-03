using Strawhenge.Inventory.Unity.Data;
using Strawhenge.Inventory.Unity.Data.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Monobehaviours
{
    public class ItemScript : MonoBehaviour
    {
        [SerializeField] ItemScriptableObject data;
        [SerializeField] EventScriptableObject[] onFixatedEvents;
        [SerializeField] EventScriptableObject[] onUnfixatedEvents;

        readonly List<Collider> ignoredColliders = new List<Collider>();
        IEnumerable<Collider> colliders;

        void Awake()
        {
            colliders = GetComponentsInChildren<Collider>();

            if (data != null)
            {
                Data = data;
                return;
            }

            Debug.LogError("Missing item data.", this);
            Data = new NullItemData();
        }

        public IItemData Data { get; private set; }

        public void Fixate(IEnumerable<Collider> bindToColliders)
        {
            ResetIgnoredColliders();
            IgnoreColliders(bindToColliders);
        }

        public void Unfixate()
        {
            ResetIgnoredColliders();
        }

        void IgnoreColliders(IEnumerable<Collider> bindToColliders)
        {
            foreach (var collider in colliders)
                foreach (var bindTo in bindToColliders)
                    Physics.IgnoreCollision(collider, bindTo, ignore: true);

            ignoredColliders.AddRange(bindToColliders);
        }

        void ResetIgnoredColliders()
        {
            foreach (var collider in colliders)
                foreach (var bindTo in ignoredColliders)
                    Physics.IgnoreCollision(collider, bindTo, ignore: false);

            ignoredColliders.Clear();
        }
    }
}
