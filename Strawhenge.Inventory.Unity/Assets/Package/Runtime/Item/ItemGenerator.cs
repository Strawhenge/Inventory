using FunctionalUtilities;
using Strawhenge.Inventory.TransientItems;
using Strawhenge.Inventory.Unity.Data;

namespace Strawhenge.Inventory.Unity.Items
{
    public class ItemGenerator : IItemGenerator
    {
        private readonly IItemFactory itemFactory;
        private readonly IItemRepository itemRepository;

        public ItemGenerator(IItemFactory itemFactory, IItemRepository itemRepository)
        {
            this.itemFactory = itemFactory;
            this.itemRepository = itemRepository;
        }

        public Maybe<IItem> GenerateByName(string name)
        {
            return itemRepository.FindByName(name)
                .Map(itemFactory.Create);
        }
    }
}