using Strawhenge.Common;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Unity.Items.ItemData;
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

        internal (Item, Context) PickupItem()
        {
            var item = GetItem();
            var context = GetContext();

            _onPickup.Invoke();

            if (_destroyOnPickup)
                Destroy(gameObject);

            return (item, context);
        }

        Item GetItem()
        {
            if (_data == null)
            {
                Debug.LogError($"Missing {nameof(_data)}.", this);

                return ItemBuilder
                    .Create(string.Empty, ItemSize.OneHanded, false, 0)
                    .Build();
            }

            return _data.ToItem();
        }

        Context GetContext()
        {
            if (_context == null)
            {
                _context = new Context();
                _contextHandlers.ForEach(x => x.Handle(_context));
            }

            return _context;
        }
    }
}