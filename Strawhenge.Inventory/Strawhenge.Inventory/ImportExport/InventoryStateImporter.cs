using System.Collections.Generic;
using Strawhenge.Common.Logging;

namespace Strawhenge.Inventory.ImportExport
{
    class InventoryStateImporter
    {
        readonly Inventory _inventory;
        readonly ILogger _logger;

        public InventoryStateImporter(Inventory inventory, ILogger logger)
        {
            _inventory = inventory;
            _logger = logger;
        }

        public void Import(InventoryState state)
        {
            ImportItems(state.Items);
            ImportApparelPieces(state.ApparelPieces);
        }

        void ImportItems(IReadOnlyList<ItemState> items)
        {
            foreach (var itemState in items)
            {
                var item = _inventory.CreateItem(itemState.Item, itemState.Context);

                switch (itemState.InHand)
                {
                    case ItemInHandState.NotInHands:
                        break;

                    case ItemInHandState.InLeftHand:
                        item.HoldLeftHand();
                        break;

                    case ItemInHandState.InRightHand:
                        item.HoldRightHand();
                        break;

                    default:
                        _logger.LogWarning($"Unknown state '{itemState.InHand}'.");
                        break;
                }

                if (itemState.IsInStorage)
                {
                    if (!item.Storable.HasSome(out var storable))
                        _logger.LogWarning($"'{itemState.Item.Name}' is not storable.");
                    else
                    {
                        var storeItemResult = storable.AddToStorage();

                        if (storeItemResult.HasInsufficientCapacity)
                            _logger.LogWarning($"'{itemState.Item.Name}' cannot be stored due to capacity.");
                    }
                }

                itemState.Holster.Do(holsterName =>
                {
                    if (item.Holsters[holsterName].HasSome(out var itemHolster))
                        itemHolster.Equip();
                    else
                        _logger.LogWarning($"'{itemState.Item.Name}' cannot be equipped to holster '{holsterName}'.");
                });
            }
        }

        void ImportApparelPieces(IReadOnlyList<ApparelPieceState> apparelPieces)
        {
            foreach (var apparelPieceState in apparelPieces)
            {
                var apparelPiece = _inventory.CreateApparelPiece(apparelPieceState.ApparelPiece);
                apparelPiece.Equip();
            }
        }
    }
}