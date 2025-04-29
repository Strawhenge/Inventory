using Strawhenge.Common;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Unity.Items.Data.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Strawhenge.Inventory.Unity.Items
{
    public class ItemPickupScript : MonoBehaviour
    {
        [SerializeField] ItemScriptableObject _data;
        [SerializeField] ItemContextHandlerScript[] _contextHandlers;
        [SerializeField] bool _destroyOnPickup = true;
        [SerializeField] UnityEvent _onPickup;

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
            if (_context == null)
            {
                _context = new Context();
                _contextHandlers.ForEach(x => x.Handle(_context));
            }

            _onPickup.Invoke();

            if (_destroyOnPickup)
                Destroy(gameObject);

            // TODO Error handling for missing scriptable object field.
            return (_data.ToItemData(), _context);
        }
    }
}