﻿using FunctionalUtilities;
using Strawhenge.Common.Logging;
using System.Collections.Generic;

namespace Strawhenge.Inventory.Unity.Loader
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
            _logger.LogInformation("Loading items into inventory.");

            foreach (var item in items)
                LoadItem(item);
        }

        void LoadItem(ILoadInventoryItem loadItem)
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

        void LoadApparel(IEnumerable<ILoadApparelPiece> apparelPieces)
        {
            _logger.LogInformation("Loading apparel into inventory.");

            foreach (var apparelPiece in apparelPieces)
                LoadApparelPiece(apparelPiece);
        }

        void LoadApparelPiece(ILoadApparelPiece loadApparelPiece)
        {
            var apparelPiece = _inventory.CreateApparelPiece(loadApparelPiece.ApparelPiece);
            apparelPiece.Equip();
        }
    }
}