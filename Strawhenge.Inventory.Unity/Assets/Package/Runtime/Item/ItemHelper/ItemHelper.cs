using Strawhenge.Inventory.Unity.Data;
using System;
using System.Collections.Generic;
using FunctionalUtilities;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items
{
    public class ItemHelper : IItemHelper
    {
        readonly ISpawner _spawner;
        readonly IReadOnlyList<Collider> _bindToColliders;

        ItemScript _script;
        bool _isFixated;

        public ItemHelper(ISpawner spawner, IReadOnlyList<Collider> bindToColliders, IItemData data)
        {
            _spawner = spawner;
            _bindToColliders = bindToColliders;
            Data = data;
        }

        public event Action Released;

        public IItemData Data { get; }

        public ItemScript Spawn()
        {
            if (_script == null)
            {
                _script = _spawner.Spawn(Data);
            }

            if (!_isFixated)
            {
                _isFixated = true;
                _script.Fixate(_bindToColliders);
            }

            return _script;
        }

        public void Despawn()
        {
            _isFixated = false;
            _spawner.Despawn(_script);
            _script = null;
        }

        public Maybe<ItemScript> Release()
        {
            if (_script == null)
                return Maybe.None<ItemScript>();

            _script.transform.parent = null;
            _script.Unfixate();
            _isFixated = false;
            Released?.Invoke();

            return _script;
        }
    }
}