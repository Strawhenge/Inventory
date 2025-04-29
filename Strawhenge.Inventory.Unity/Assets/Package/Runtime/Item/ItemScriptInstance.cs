using Strawhenge.Inventory.Unity.Items;
using Object = UnityEngine.Object;

namespace Strawhenge.Inventory.Unity.Items
{
    public class ItemScriptInstance
    {
        readonly ItemScript _prefab;
        readonly Context _context;

        ItemScript _script;

        public ItemScriptInstance(ItemScript prefab) : this(prefab, new Context())
        {
        }

        public ItemScriptInstance(ItemScript prefab, Context context)
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