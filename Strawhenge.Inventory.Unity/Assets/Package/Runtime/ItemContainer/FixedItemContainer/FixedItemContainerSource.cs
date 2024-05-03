using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Data;
using Strawhenge.Inventory.Unity.Data.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;

namespace Strawhenge.Inventory.Unity
{
    public class FixedItemContainerSource : IItemContainerSource
    {
        readonly List<IItemData> _items;
        readonly List<IApparelPieceData> _apparelPieces;

        public FixedItemContainerSource(
            IEnumerable<IItemData> items,
            IEnumerable<IApparelPieceData> apparelPieces)
        {
            _items = items.ToList();
            _apparelPieces = apparelPieces.ToList();
        }

        public void Add(IItemData item) => _items.Add(item);

        public void Add(IApparelPieceData apparelPiece) => _apparelPieces.Add(apparelPiece);

        public IReadOnlyList<IContainedItem<IItemData>> GetItems() =>
            _items
                .Select(item =>
                    new ContainedItem<IItemData>(
                        item,
                        removeStrategy: () => _items.Remove(item)))
                .ToArray();

        public IReadOnlyList<IContainedItem<IApparelPieceData>> GetApparelPieces() =>
            _apparelPieces
                .Select(apparelPiece =>
                    new ContainedItem<IApparelPieceData>(
                        apparelPiece,
                        removeStrategy: () => _apparelPieces.Remove(apparelPiece)))
                .ToArray();
    }
}