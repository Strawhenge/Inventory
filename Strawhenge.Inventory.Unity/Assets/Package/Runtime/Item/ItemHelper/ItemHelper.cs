using Strawhenge.Inventory.Unity.Data;
using System;
using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Common.Unity;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Strawhenge.Inventory.Unity.Items
{
    public class ItemHelper : IItemHelper
    {
        readonly IItemDropPoint _itemDropPoint;
        ItemScript _script;

        public ItemHelper(IItemData data, IItemDropPoint itemDropPoint)
        {
            _itemDropPoint = itemDropPoint;
            Data = data;
        }

        public event Action Released;

        public IItemData Data { get; }

        public ItemScript Spawn()
        {
            if (_script == null)
            {
                _script = Object.Instantiate(Data.Prefab);
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

        public Maybe<ItemPickupScript> Release()
        {
            var pickup = Data.PickupPrefab.Map(pickupPrefab =>
            {
                var spawnPoint = _script == null
                    ? _itemDropPoint.GetPoint()
                    : _script.transform.GetPositionAndRotation();

                return Object.Instantiate(pickupPrefab, spawnPoint.Position, spawnPoint.Rotation);
            });

            Despawn();
            Released?.Invoke();
            return pickup;
        }
    }
}