using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Unity.Data;

namespace Strawhenge.Inventory.Unity.Loader
{
    public class InventoryLoader
    {
        readonly ItemManager _itemManager;
        readonly IItemRepository _itemRepository;
        readonly ILogger _logger;

        public InventoryLoader(
            ItemManager itemManager,
            IItemRepository itemRepository,
            ILogger logger)
        {
            _itemManager = itemManager;
            _itemRepository = itemRepository;
            _logger = logger;
        }

        public void Load(InventoryLoadData data)
        {
            LoadHolsteredItems(data.HolsteredItems);
        }

        void LoadHolsteredItems(HolsteredItemLoadDataEntry[] itemsData)
        {
            _logger.LogInformation("Loading holstered items.");

            foreach (var itemData in itemsData)
            {
                var itemResult = _itemRepository.FindByName(itemData.ItemName);

                if (!itemResult.HasSome(out var item))
                {
                    _logger.LogError($"Item '{itemData.ItemName}' cannot be found.");
                    continue;
                }

                _itemManager
                    .Manage(item)
                    .Holsters
                    .FirstOrNone(x => x.HolsterName == itemData.HolsterName)
                    .Do(x => x.Equip());
            }
        }
    }
}