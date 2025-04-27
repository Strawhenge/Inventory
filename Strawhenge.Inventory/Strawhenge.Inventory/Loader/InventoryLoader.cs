using FunctionalUtilities;
using Strawhenge.Common.Logging;
using System.Collections.Generic;

namespace Strawhenge.Inventory.Loader
{
    public class InventoryLoader
    {
        readonly Inventory _inventory;
        readonly ILogger _logger;

        public InventoryLoader(Inventory inventory, ILogger logger)
        {
            _inventory = inventory;
            _logger = logger;
        }

        public void Load(LoadInventoryData data)
        {
            LoadItems(data.Items);
            LoadApparel(data.ApparelPieces);
        }

        void LoadItems(IReadOnlyList<LoadInventoryItem> items)
        {
            _logger.LogInformation("Loading items into inventory.");

            foreach (var item in items)
                LoadItem(item);
        }

        void LoadItem(LoadInventoryItem loadItem)
        {
            var item = _inventory.CreateItem(loadItem.ItemData);

            loadItem.HolsterName.Do(holsterName =>
                item.Holsters.FirstOrNone(x => x.HolsterName == holsterName).Do(y => y.Equip()));

            if (loadItem.IsInStorage)
                item.Storable.Do(x => x.AddToStorage());

            if (loadItem.InHand == LoadInventoryItemInHand.Left)
                item.HoldLeftHand();
            else if (loadItem.InHand == LoadInventoryItemInHand.Right)
                item.HoldRightHand();
        }

        void LoadApparel(IEnumerable<LoadApparelPiece> apparelPieces)
        {
            _logger.LogInformation("Loading apparel into inventory.");

            foreach (var apparelPiece in apparelPieces)
                LoadApparelPiece(apparelPiece);
        }

        void LoadApparelPiece(LoadApparelPiece loadApparelPiece)
        {
            var apparelPiece = _inventory.CreateApparelPiece(loadApparelPiece.ApparelPiece);
            apparelPiece.Equip();
        }
    }
}