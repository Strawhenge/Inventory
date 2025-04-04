using FunctionalUtilities;
using Strawhenge.Inventory.Unity.Items.Data;
using System;
using Strawhenge.Common.Unity;
using Strawhenge.Inventory.Unity.Items.Context;
using Object = UnityEngine.Object;

namespace Strawhenge.Inventory.Unity.Items
{
    public class ItemHelper
    {
        readonly IItemDropPoint _itemDropPoint;
        readonly ItemContext _context;
        ItemScript _script;

        public ItemHelper(IItemData data, IItemDropPoint itemDropPoint)
            : this(data, itemDropPoint, new ItemContext())
        {
        }

        public ItemHelper(IItemData data, IItemDropPoint itemDropPoint, ItemContext context)
        {
            _itemDropPoint = itemDropPoint;
            _context = context;
            Data = data;
        }

        public event Action Released;

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

        public Maybe<ItemPickupScript> Release()
        {
            var pickup = Data.PickupPrefab
                .Map(pickupPrefab =>
                {
                    var spawnPoint = _script == null
                        ? _itemDropPoint.GetPoint()
                        : _script.transform.GetPositionAndRotation();

                    return Object.Instantiate(pickupPrefab, spawnPoint.Position, spawnPoint.Rotation);
                });

            Despawn();
            Released?.Invoke();

            return pickup
                .Do(p => p.ContextIn(_context));
        }
    }
}