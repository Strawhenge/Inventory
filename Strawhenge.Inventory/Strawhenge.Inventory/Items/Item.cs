using System.Collections.Generic;
using System.Linq;
using FunctionalUtilities;

namespace Strawhenge.Inventory.Items
{
    public class Item
    {
        readonly GenericData _genericData;

        internal Item(
            string name,
            ItemSize size,
            bool isStorable,
            int weight,
            Maybe<ItemConsumable> consumable,
            IEnumerable<ItemHolster> holsters,
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

        public Maybe<ItemConsumable> Consumable { get; }

        public IReadOnlyList<ItemHolster> Holsters { get; }

        public Maybe<T> Get<T>() where T : class => _genericData.Get<T>();
    }
}