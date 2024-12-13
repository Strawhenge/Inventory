using Strawhenge.Common;
using Strawhenge.Inventory.Unity.Items.Context;
using Strawhenge.Inventory.Unity.Items.Data;
using Strawhenge.Inventory.Unity.Items.Data.ScriptableObjects;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items
{
    public class ItemPickupScript : MonoBehaviour
    {
        [SerializeField] ItemScriptableObject _data;
        [SerializeField] ItemContextHandlerScript[] _contextHandlers;

        IItemData _itemData;

        void Awake()
        {
            _itemData = _data;

            if (_itemData == null)
            {
                Debug.LogError($"Missing {nameof(_data)}.", this);
                _itemData = new NullItemData();
            }
        }

        internal IItemData PickupItem()
        {
            OnPickup();
            return _data;
        }

        /// <summary>
        /// Called when pickup is invoked. Destroys the GameObject, unless overriden.
        /// </summary>
        protected virtual void OnPickup() => Destroy(gameObject);

        internal void ContextIn(ItemContext itemContext) =>
            _contextHandlers.ForEach(handler => handler.ContextIn(itemContext));

        internal void ContextOut(ItemContext itemContext) =>
            _contextHandlers.ForEach(handler => handler.ContextOut(itemContext));
    }
}