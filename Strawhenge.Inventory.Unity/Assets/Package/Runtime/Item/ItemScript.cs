using Strawhenge.Common.Unity;
using Strawhenge.Inventory.Unity.Data;
using Strawhenge.Inventory.Unity.Data.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Strawhenge.Inventory.Unity.Items
{
    public class ItemScript : MonoBehaviour
    {
        [FormerlySerializedAs("data"), SerializeField]
        ItemScriptableObject _data;

        [FormerlySerializedAs("onFixatedEvents"), SerializeField]
        EventScriptableObject[] _onFixatedEvents;

        [SerializeField] UnityEvent _fixated;

        [FormerlySerializedAs("onUnfixatedEvents"), SerializeField]
        EventScriptableObject[] _onUnfixatedEvents;

        [SerializeField] UnityEvent _unfixated;

        void Awake()
        {
            if (_data != null)
            {
                Data = _data;
                return;
            }

            Debug.LogError("Missing item data.", this);
            Data = new NullItemData();
        }

        public IItemData Data { get; private set; }

        public void Fixate()
        {
            _fixated.Invoke();

            foreach (var onFixate in _onFixatedEvents)
            {
                if (onFixate != null)
                    onFixate.Invoke(gameObject);
            }
        }

        public void Unfixate()
        {
            _unfixated.Invoke();

            foreach (var onUnfixate in _onUnfixatedEvents)
            {
                if (onUnfixate != null)
                    onUnfixate.Invoke(gameObject);
            }
        }
    }
}