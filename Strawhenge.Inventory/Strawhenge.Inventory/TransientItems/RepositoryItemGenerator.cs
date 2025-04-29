using FunctionalUtilities;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory.TransientItems
{
    public class RepositoryItemGenerator : IItemGenerator
    {
        readonly Inventory _inventory;
        readonly IItemRepository _itemRepository;

        public RepositoryItemGenerator(Inventory inventory, IItemRepository itemRepository)
        {
            _inventory = inventory;
            _itemRepository = itemRepository;
        }

        public Maybe<Item> GenerateByName(string name)
        {
            return _itemRepository
                .FindByName(name)
                .Map(data => _inventory.CreateTransientItem(data));
        }
    }
}