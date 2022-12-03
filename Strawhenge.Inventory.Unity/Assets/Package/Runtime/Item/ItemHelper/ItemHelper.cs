using Strawhenge.Inventory.Unity.Data;
using Strawhenge.Inventory.Unity.Monobehaviours;
using System;
using System.Collections.Generic;
using FunctionalUtilities;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items
{
    public class ItemHelper : IItemHelper
    {
        private readonly ISpawner spawner;
        private readonly IEnumerable<Collider> bindToColliders;

        ItemScript script;
        bool isFixated;

        public ItemHelper(ISpawner spawner, IEnumerable<Collider> bindToColliders, ItemScript script) : this(spawner, bindToColliders, script.Data)
        {
            this.script = script;
            script.Fixate(bindToColliders);
        }

        public ItemHelper(ISpawner spawner, IEnumerable<Collider> bindToColliders, IItemData data)
        {
            this.spawner = spawner;
            this.bindToColliders = bindToColliders;
            Data = data;
        }

        public event Action Released;

        public IItemData Data { get; }

        public ItemScript Spawn()
        {
            if (script == null)
            {
                script = spawner.Spawn(Data);
            }

            if (!isFixated)
            {
                isFixated = true;
                script.Fixate(bindToColliders);
            }

            return script;
        }

        public void Despawn()
        {
            isFixated = false;
            spawner.Despawn(script);
            script = null;
        }

        public Maybe<ItemScript> Release()
        {
            if (script == null)
                return Maybe.None<ItemScript>();

            script.transform.parent = null;
            script.Unfixate();
            isFixated = false;
            Released?.Invoke();

            return script;
        }
    }
}
