using Strawhenge.Inventory.Unity.Items;
using UnityEngine;
using ILogger = Strawhenge.Common.Logging.ILogger;

namespace Strawhenge.Inventory.Unity.Components
{
    public class HolsterComponent : IHolsterComponent
    {
        readonly Transform transform;
        readonly ILogger logger;

        IItemHelper item;

        public HolsterComponent(string name, Transform transform, ILogger logger)
        {
            this.transform = transform;
            this.logger = logger;

            Name = name;
        }

        public string Name { get; }

        public void SetItem(IItemHelper item)
        {
            this.item = item;

            var data = item.GetHolsterData(this, logger);

            var itemScript = item.Spawn();
            itemScript.transform.parent = transform;
            itemScript.transform.localPosition = data.PositionOffset;
            itemScript.transform.localRotation = data.RotationOffset;
        }

        public IItemHelper TakeItem()
        {
            if (item == null)
            {
                logger.LogError("No item in holster.");
                return new NullItemHelper();
            }

            var result = item;
            item = null;
            return result;
        }
    }
}