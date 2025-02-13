using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.TransientItems;

namespace Strawhenge.Inventory.Tests._new
{
    public class ItemGeneratorFake : IItemGenerator
    {
        readonly Dictionary<string, IItem> _items = new Dictionary<string, IItem>();

        public Maybe<IItem> GenerateByName(string name)
        {
            return _items.TryGetValue(name, out var item)
                ? Maybe.Some(item)
                : Maybe.None<IItem>();
        }

        public void Set(string name, IItem item) => _items[name] = item;
    }
}