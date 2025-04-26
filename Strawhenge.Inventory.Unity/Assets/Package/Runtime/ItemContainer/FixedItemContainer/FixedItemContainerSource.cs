using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Unity.Apparel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Strawhenge.Inventory.Unity
{
    public class FixedItemContainerSource : IItemContainerSource, IFixedItemContainerInfo
    {
        readonly List<ItemData> _items;
        readonly List<IApparelPieceData> _apparelPieces;

        public FixedItemContainerSource(
            IEnumerable<ItemData> items,
            IEnumerable<IApparelPieceData> apparelPieces)
        {
            _items = items.ToList();
            _apparelPieces = apparelPieces.ToList();
        }

        public event Action StateChanged;

        public int Count => _items.Count + _apparelPieces.Count;

        public void Add(ItemData item)
        {
            _items.Add(item);
            StateChanged?.Invoke();
        }

        public void Add(IApparelPieceData apparelPiece)
        {
            _apparelPieces.Add(apparelPiece);
            StateChanged?.Invoke();
        }

        public IReadOnlyList<IContainedItem<ItemData>> GetItems() =>
            _items
                .Select(item =>
                    new ContainedItem<ItemData>(
                        item,
                        removeStrategy: () =>
                        {
                            _items.Remove(item);
                            StateChanged?.Invoke();
                        }))
                .ToArray();

        public IReadOnlyList<IContainedItem<IApparelPieceData>> GetApparelPieces() =>
            _apparelPieces
                .Select(apparelPiece =>
                    new ContainedItem<IApparelPieceData>(
                        apparelPiece,
                        removeStrategy: () =>
                        {
                            _apparelPieces.Remove(apparelPiece);
                            StateChanged?.Invoke();
                        }))
                .ToArray();

        public FixedItemContainerSource Clone() => new(_items, _apparelPieces);

        public void Merge(FixedItemContainerSource source)
        {
            _items.AddRange(source._items);
            _apparelPieces.AddRange(source._apparelPieces);
            StateChanged?.Invoke();
        }
    }
}