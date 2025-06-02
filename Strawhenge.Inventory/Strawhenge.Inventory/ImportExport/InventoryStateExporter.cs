using System.Collections.Generic;
using System.Linq;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory.ImportExport
{
    class InventoryStateExporter
    {
        readonly Inventory _inventory;

        public InventoryStateExporter(Inventory inventory)
        {
            _inventory = inventory;
        }

        public InventoryState Export()
        {
            return new InventoryState(
                ExportItems(),
                ExportApparelPieces());
        }

        IEnumerable<ItemState> ExportItems()
        {
            return _inventory.StoredItems.Items
                .Concat(_inventory.Holsters.SelectMany(x => x.CurrentItem.AsEnumerable()))
                .Concat(_inventory.Hands.LeftHand.CurrentItem.AsEnumerable())
                .Concat(_inventory.Hands.RightHand.CurrentItem.AsEnumerable())
                .Distinct()
                .Select(item =>
                {
                    var inHand = item.IsInLeftHand
                        ? ItemInHandState.InLeftHand
                        : item.IsInRightHand
                            ? ItemInHandState.InRightHand
                            : ItemInHandState.NotInHands;

                    var holster = item.Holsters.IsEquippedToHolster(out InventoryItemHolster itemHolster)
                        ? itemHolster.HolsterName
                        : null;

                    return new ItemState(item.Item, item.Context, inHand, holster, item.IsInStorage);
                });
        }

        IEnumerable<ApparelPieceState> ExportApparelPieces()
        {
            return _inventory.ApparelSlots
                .SelectMany(slot => slot.CurrentPiece.AsEnumerable())
                .Select(apparelPiece => new ApparelPieceState(apparelPiece.Piece));
        }
    }
}