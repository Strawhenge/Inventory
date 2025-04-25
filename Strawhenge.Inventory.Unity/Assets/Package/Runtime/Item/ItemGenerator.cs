using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.TransientItems;
using Strawhenge.Inventory.Unity.Items.Data;
using Strawhenge.Inventory.Unity.Items.Data.ScriptableObjects;

namespace Strawhenge.Inventory.Unity.Items
{
    public class ItemGenerator : IItemGenerator
    {
        readonly Strawhenge.Inventory.Inventory _inventory;
        readonly IItemRepository _itemRepository;

        public ItemGenerator(Strawhenge.Inventory.Inventory inventory, IItemRepository itemRepository)
        {
            _inventory = inventory;
            _itemRepository = itemRepository;
        }

        public Maybe<Item> GenerateByName(string name)
        {
            return _itemRepository
                .FindByName(name)
                .Map(data =>
                {
                    var itemData = (data as ItemScriptableObject).ToItemData();
                    return _inventory.CreateTransientItem(itemData);
                });
        }
    }
}