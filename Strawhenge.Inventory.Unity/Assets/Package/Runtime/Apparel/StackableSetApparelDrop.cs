using Strawhenge.Inventory.Unity.Menu;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ILogger = Strawhenge.Common.Logging.ILogger;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public class StackableSetApparelDrop : IApparelDrop, ISetApparelContainerPrefab
    {
        readonly Queue<IApparelPieceData> _queue = new Queue<IApparelPieceData>();
        readonly IInventoryMenu _inventoryMenu;
        readonly IItemContainerMenu _itemContainerMenu;
        readonly ItemDropPoint _itemDropPoint;
        readonly ILogger _logger;

        FixedItemContainerScript _containerPrefab;

        public StackableSetApparelDrop(
            IInventoryMenu inventoryMenu,
            IItemContainerMenu itemContainerMenu,
            ItemDropPoint itemDropPoint,
            ILogger logger)
        {
            _inventoryMenu = inventoryMenu;
            _itemContainerMenu = itemContainerMenu;
            _itemDropPoint = itemDropPoint;
            _logger = logger;

            _inventoryMenu.Closed += StateChanged;
            _itemContainerMenu.Closed += StateChanged;
        }

        public void Drop(IApparelPieceData data)
        {
            if (ReferenceEquals(_containerPrefab, null))
            {
                _logger.LogError($"{nameof(FixedItemContainerScript)} is missing from apparel drop.");
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

        public void Set(FixedItemContainerScript prefab) => _containerPrefab = prefab;

        void StateChanged()
        {
            if (ShouldWait() || !_queue.Any())
                return;

            var container = CreateContainer();

            while (_queue.Any())
                container.Add(_queue.Dequeue());
        }

        bool ShouldWait() => _inventoryMenu.IsOpen || _itemContainerMenu.IsOpen;

        FixedItemContainerScript CreateContainer() =>
            Object.Instantiate(
                _containerPrefab,
                position: _itemDropPoint.GetPoint(),
                rotation: Quaternion.Euler(Vector3.forward));
    }
}