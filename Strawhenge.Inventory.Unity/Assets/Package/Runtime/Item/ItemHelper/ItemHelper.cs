using Strawhenge.Inventory.Unity.Items.Context;
using Strawhenge.Inventory.Unity.Items.Data;
using Object = UnityEngine.Object;

namespace Strawhenge.Inventory.Unity.Items
{
    public class ItemHelper
    {
        readonly ItemContext _context;
        ItemScript _script;

        public ItemHelper(IItemData data)
            : this(data, new ItemContext())
        {
        }

        public ItemHelper(IItemData data, ItemContext context)
        {
            _context = context;
            Data = data;
        }

        public IItemData Data { get; }

        public ItemScript Spawn()
        {
            if (_script == null)
            {
                _script = Object.Instantiate(Data.Prefab);
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