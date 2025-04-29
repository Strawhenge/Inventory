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

        Context _context;

        void Awake()
        {
            if (_data == null)
                Debug.LogError($"Missing {nameof(_data)}.", this);
        }

        internal void SetContext(Context context)
        {
            _context = context;
            _contextHandlers.ForEach(x => x.Handle(_context));
        }

        internal (ItemData, Context) PickupItem()
        {
            OnPickup();

            if (_context == null)
            {
                _context = new Context();
                _contextHandlers.ForEach(x => x.Handle(_context));
            }

            // TODO Error handling for missing scriptable object field.
            return (_data.ToItemData(), _context);
        }

        /// <summary>
        /// Called when pickup is invoked. Destroys the GameObject, unless overriden.
        /// </summary>
        protected virtual void OnPickup() => Destroy(gameObject);
    }
}