using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Unity.Menu;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ILogger = Strawhenge.Common.Logging.ILogger;

namespace Strawhenge.Inventory.Unity.Loot
{
    public class LootDrop :  ISetLootDropPrefab
    {
        readonly Queue<ApparelPieceData> _queue = new();
        readonly IInventoryMenu _inventoryMenu;
        readonly ILootMenu _itemContainerMenu;
        readonly DropPoint _dropPoint;
        readonly ILogger _logger;

        LootCollectionScript _containerPrefab;

        public LootDrop(
            IInventoryMenu inventoryMenu,
            ILootMenu itemContainerMenu,
            DropPoint dropPoint,
            ILogger logger)
        {
            _inventoryMenu = inventoryMenu;
            _itemContainerMenu = itemContainerMenu;
            _dropPoint = dropPoint;
            _logger = logger;

            _inventoryMenu.Closed += StateChanged;
            _itemContainerMenu.Closed += StateChanged;
        }

        public void Drop(ApparelPieceData data)
        {
            if (ReferenceEquals(_containerPrefab, null))
            {
                _logger.LogError($"'{nameof(LootCollectionScript)}' is missing from apparel drop.");
                return;
            }

            if (ShouldWait())
            {
                _queue.Enqueue(data);
                return;
            }

            var container = CreateContainer();
            container.Add(data);
        }

        public void Set(LootCollectionScript prefab) => _containerPrefab = prefab;

        void StateChanged()
        {
            if (ShouldWait() || !_queue.Any())
                return;

            var container = CreateContainer();

            while (_queue.Any())
                container.Add(_queue.Dequeue());
        }

        bool ShouldWait() => _inventoryMenu.IsOpen || _itemContainerMenu.IsOpen;

        LootCollectionScript CreateContainer()
        {
            var spawnPoint = _dropPoint.GetPoint();
            return Object.Instantiate(_containerPrefab, spawnPoint.Position, spawnPoint.Rotation);
        }
    }
}