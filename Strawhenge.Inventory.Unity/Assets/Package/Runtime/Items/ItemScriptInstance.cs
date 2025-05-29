using Object = UnityEngine.Object;

namespace Strawhenge.Inventory.Unity.Items
{
    public class ItemScriptInstance
    {
        readonly ItemScript _prefab;
        readonly Context _context;
        readonly PrefabInstantiatedEvents _prefabInstantiatedEvents;

        ItemScript _script;

        public ItemScriptInstance(ItemScript prefab, Context context, PrefabInstantiatedEvents prefabInstantiatedEvents)
        {
            _prefab = prefab;
            _context = context;
            _prefabInstantiatedEvents = prefabInstantiatedEvents;
        }

        public ItemScript Spawn()
        {
            if (_script == null)
            {
                _script = Object.Instantiate(_prefab);
                _prefabInstantiatedEvents.Invoke(_script);

                _script.SetContext(_context);
            }

            return _script;
        }

        public void Despawn()
        {
            if (_script == null)
                return;

            Object.Destroy(_script.gameObject);
            _script = null;
        }
    }
}