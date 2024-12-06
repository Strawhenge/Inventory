using Strawhenge.Inventory.Unity.Items.Data;
using Strawhenge.Inventory.Unity.Items.Data.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Strawhenge.Inventory.Unity.Items
{
    public class ItemPickupScript : MonoBehaviour
    {
        [SerializeField] ItemScriptableObject _data;
        [SerializeField, Tooltip("Destroy the GameObject on pickup.")] bool _destroyOnPickup;
        [SerializeField] UnityEvent _onPickup;
        
        void Awake()
        {
            if (_data == null)
            {
                Debug.LogError($"Missing {nameof(_data)}.", this);
                Destroy(this);
            }
        }

        internal IItemData PickupItem()
        {
            if (_destroyOnPickup)
                Destroy(gameObject);

            _onPickup.Invoke();
            return _data;
        }
    }
}