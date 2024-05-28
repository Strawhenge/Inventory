using FunctionalUtilities;
using Strawhenge.Inventory.Unity.Items.Consumables;
using Strawhenge.Inventory.Unity.Monobehaviours;
using System.Collections.Generic;
using System.Linq;

namespace Strawhenge.Inventory.Unity.Data
{
    public class NullItemData : IItemData
    {
        public string Name => string.Empty;

        public ItemScript Prefab => null;

        public ItemSize Size => ItemSize.OneHanded;

        public IHoldItemData LeftHandHoldData => new NullHoldItemData();

        public IHoldItemData RightHandHoldData => new NullHoldItemData();

        public IEnumerable<IHolsterItemData> HolsterItemData => Enumerable.Empty<IHolsterItemData>();

        public Maybe<IConsumableData> ConsumableData => Maybe.None<IConsumableData>();
    }
}