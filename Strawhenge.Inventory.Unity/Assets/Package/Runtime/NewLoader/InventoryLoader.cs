using Strawhenge.Common.Logging;
using System.Collections.Generic;

namespace Strawhenge.Inventory.Unity.NewLoader
{
    public class InventoryLoader
    {
        readonly IInventory _inventory;
        readonly ILogger _logger;

        public InventoryLoader(IInventory inventory, ILogger logger)
        {
            _inventory = inventory;
            _logger = logger;
        }

        public void Load(LoadInventoryData data)
        {
            LoadItems(data.Items);
            LoadApparel(data.ApparelPieces);
        }

        void LoadItems(IReadOnlyList<ILoadInventoryItem> items)
        {
            throw new System.NotImplementedException();
        }

        void LoadApparel(IEnumerable<ILoadApparelPiece> apparelPieces)
        {
            throw new System.NotImplementedException();
        }
    }
}