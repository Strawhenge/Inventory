using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Unity.Menu;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Loot
{
    public class LootDrop
    {
        readonly Queue<ApparelPiece> _queue = new();
        readonly LootCollectionScript _prefab;
        readonly IInventoryMenu _inventoryMenu;
        readonly ILootMenu _lootMenu;
        readonly DropPoint _dropPoint;

        public LootDrop(
            LootCollectionScript prefab,
            IInventoryMenu inventoryMenu,
            ILootMenu lootMenu,
            DropPoint dropPoint)
        {
            _prefab = prefab;
            _inventoryMenu = inventoryMenu;
            _lootMenu = lootMenu;
            _dropPoint = dropPoint;

            _inventoryMenu.Closed += StateChanged;
            _lootMenu.Closed += StateChanged;
        }

        public void Drop(ApparelPiece apparelPiece)
        {
            if (ShouldWait())
            {
                _queue.Enqueue(apparelPiece);
                return;
            }

            var container = CreateContainer();
            container.Add(apparelPiece);
        }

        void StateChanged()
        {
            if (ShouldWait() || !_queue.Any())
                return;

            var container = CreateContainer();

            while (_queue.Any())
                container.Add(_queue.Dequeue());
        }
      
        bool ShouldWait() => _inventoryMenu.IsOpen || _lootMenu.IsOpen;

        LootCollectionScript CreateContainer()
        {
            var spawnPoint = _dropPoint.GetPoint();
            return Object.Instantiate(_prefab, spawnPoint.Position, spawnPoint.Rotation);
        }
    }
}