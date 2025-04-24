using System.Collections.Generic;
using System.Linq;
using FunctionalUtilities;

namespace Strawhenge.Inventory.Items
{
    public class ItemData
    {
        readonly GenericData _genericData;

        internal ItemData(
            string name,
            ItemSize size,
            bool isStorable,
            int weight,
            Maybe<ConsumableItemData> consumable,
            IEnumerable<HolsterItemData> holsters,
            GenericData genericData)
        {
            Name = name;
            Size = size;
            IsStorable = isStorable;
            Weight = weight;
            Consumable = consumable;
            Holsters = holsters.ToArray();

            _genericData = genericData;
        }

        public string Name { get; }

        public ItemSize Size { get; }

        public bool IsStorable { get; }

        public int Weight { get; }

        public Maybe<ConsumableItemData> Consumable { get; }

        public IReadOnlyList<HolsterItemData> Holsters { get; }

        public Maybe<T> Get<T>() where T : class => _genericData.Get<T>();
    }
}