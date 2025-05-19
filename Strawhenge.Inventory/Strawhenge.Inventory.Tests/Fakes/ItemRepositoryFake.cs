using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory.Tests
{
    public class ItemRepositoryFake : IItemRepository
    {
        readonly List<Item> _items = new List<Item>();

        public Maybe<Item> FindByName(string name) =>
            _items.FirstOrNone(x => x.Name == name);

        public void Add(Item item) => _items.Add(item);
    }
}