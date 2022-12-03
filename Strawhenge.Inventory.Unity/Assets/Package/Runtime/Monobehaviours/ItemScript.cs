using Strawhenge.Common.Unity;
using Strawhenge.Inventory.Unity.Data;
using Strawhenge.Inventory.Unity.Data.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Monobehaviours
{
    public class ItemScript : MonoBehaviour
    {
        [SerializeField] ItemScriptableObject data;
        [SerializeField] EventScriptableObject[] onFixatedEvents;
        [SerializeField] EventScriptableObject[] onUnfixatedEvents;

        readonly List<Collider> _ignoredColliders = new List<Collider>();
        IEnumerable<Collider> _colliders;

        void Awake()
        {
            _colliders = GetComponentsInChildren<Collider>();

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

            foreach (var onFixate in onFixatedEvents)
            {
                if (onFixate != null)
                    onFixate.Invoke(gameObject);
            }
        }

        public void Unfixate()
        {
            ResetIgnoredColliders();

            foreach (var onUnfixate in onUnfixatedEvents)
            {
                if (onUnfixate != null)
                    onUnfixate.Invoke(gameObject);
            }
        }

        void IgnoreColliders(IEnumerable<Collider> colliders)
        {
            var bindToColliders = colliders.ToArray();

            foreach (var collider in _colliders)
            foreach (var bindTo in bindToColliders)
                Physics.IgnoreCollision(collider, bindTo, ignore: true);

            _ignoredColliders.AddRange(bindToColliders);
        }

        void ResetIgnoredColliders()
        {
            foreach (var collider in _colliders)
            foreach (var bindTo in _ignoredColliders)
                Physics.IgnoreCollision(collider, bindTo, ignore: false);

            _ignoredColliders.Clear();
        }
    }
}