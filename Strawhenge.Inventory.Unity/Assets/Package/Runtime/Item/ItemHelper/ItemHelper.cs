using Strawhenge.Inventory.Unity.Items.Context;
using Object = UnityEngine.Object;

namespace Strawhenge.Inventory.Unity.Items
{
    public class ItemHelper
    {
        readonly ItemScript _prefab;
        readonly ItemContext _context;

        ItemScript _script;

        public ItemHelper(ItemScript prefab) : this(prefab, new ItemContext())
        {
        }

        public ItemHelper(ItemScript prefab, ItemContext context)
        {
            _prefab = prefab;
            _context = context;
        }

        public ItemScript Spawn()
        {
            if (_script == null)
            {
                _script = Object.Instantiate(_prefab);
                _script.ContextIn(_context);
            }

            return _script;
        }

        public void Despawn()
        {
            if (_script == null)
                return;

            _script.ContextOut(_context);
            Object.Destroy(_script.gameObject);
            _script = null;
        }
    }
}