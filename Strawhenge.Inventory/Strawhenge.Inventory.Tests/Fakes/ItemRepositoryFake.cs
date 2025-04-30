using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory.Tests
{
    public class ItemRepositoryFake : IItemRepository
    {
        readonly List<ItemData> _items = new List<ItemData>();

        public Maybe<ItemData> FindByName(string name) =>
            _items.FirstOrNone(x => x.Name == name);

        public void Add(ItemData item) => _items.Add(item);
    }
}