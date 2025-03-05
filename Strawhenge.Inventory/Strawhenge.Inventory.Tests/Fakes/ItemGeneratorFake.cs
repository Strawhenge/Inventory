using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.TransientItems;

namespace Strawhenge.Inventory.Tests
{
    public class ItemGeneratorFake : IItemGenerator
    {
        readonly Dictionary<string, Item> _items = new Dictionary<string, Item>();

        public Maybe<Item> GenerateByName(string name)
        {
            return _items.TryGetValue(name, out var item)
                ? Maybe.Some(item)
                : Maybe.None<Item>();
        }

        public void Set(string name, Item item) => _items[name] = item;
    }
}