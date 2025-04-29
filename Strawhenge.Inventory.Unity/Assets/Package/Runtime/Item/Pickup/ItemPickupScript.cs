using Strawhenge.Common;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Unity.Items.Data.ScriptableObjects;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items
{
    public class ItemPickupScript : MonoBehaviour
    {
        [SerializeField] ItemScriptableObject _data;
        [SerializeField] ItemContextHandlerScript[] _contextHandlers;

        void Awake()
        {
            if (_data == null)
                Debug.LogError($"Missing {nameof(_data)}.", this);
        }

        internal ItemData PickupItem()
        {
            OnPickup();
            return _data.ToItemData(); // TODO Error handling for missing scriptable object field.
        }

        /// <summary>
        /// Called when pickup is invoked. Destroys the GameObject, unless overriden.
        /// </summary>
        protected virtual void OnPickup() => Destroy(gameObject);

        internal void ContextIn(Context itemContext) =>
            _contextHandlers.ForEach(handler => handler.ContextIn(itemContext));

        internal void ContextOut(Context itemContext) =>
            _contextHandlers.ForEach(handler => handler.ContextOut(itemContext));
    }
}