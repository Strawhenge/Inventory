using Strawhenge.Inventory.Unity.Items;
using UnityEngine;
using ILogger = Strawhenge.Common.Logging.ILogger;

namespace Strawhenge.Inventory.Unity.Components
{
    public class HolsterComponent : IHolsterComponent
    {
        readonly Transform _transform;
        readonly ILogger _logger;

        IItemHelper _item;

        public HolsterComponent(string name, Transform transform, ILogger logger)
        {
            _transform = transform;
            _logger = logger;

            Name = name;
        }

        public string Name { get; }

        public void SetItem(IItemHelper item)
        {
            _item = item;

            var data = item.GetHolsterData(this, _logger);

            var itemScript = item.Spawn();
            itemScript.transform.parent = _transform;
            itemScript.transform.localPosition = data.PositionOffset;
            itemScript.transform.localRotation = data.RotationOffset;
        }

        public IItemHelper TakeItem()
        {
            if (_item == null)
            {
                _logger.LogError("No item in holster.");
                return new NullItemHelper();
            }

            var result = _item;
            _item = null;
            return result;
        }
    }
}