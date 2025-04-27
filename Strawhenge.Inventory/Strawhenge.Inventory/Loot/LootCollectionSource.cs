using Strawhenge.Common;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Strawhenge.Inventory.Loot
{
    public class LootCollectionSource : ILootSource, ILootCollectionInfo
    {
        readonly List<ItemData> _items;
        readonly List<ApparelPieceData> _apparelPieces;

        public LootCollectionSource(
            IEnumerable<ItemData> items = null,
            IEnumerable<ApparelPieceData> apparelPieces = null)
        {
            _items = items
                .ExcludeNull()
                .ToList();

            _apparelPieces = apparelPieces
                .ExcludeNull()
                .ToList();
        }

        public event Action StateChanged;

        public int Count => _items.Count + _apparelPieces.Count;

        public void Add(ItemData item)
        {
            _items.Add(item);
            StateChanged?.Invoke();
        }

        public void Add(ApparelPieceData apparelPiece)
        {
            _apparelPieces.Add(apparelPiece);
            StateChanged?.Invoke();
        }

        public IReadOnlyList<Loot<ItemData>> GetItems() =>
            _items
                .Select(item =>
                    new Loot<ItemData>(
                        item,
                        onTake: () =>
                        {
                            _items.Remove(item);
                            StateChanged?.Invoke();
                        }))
                .ToArray();

        public IReadOnlyList<Loot<ApparelPieceData>> GetApparelPieces() =>
            _apparelPieces
                .Select(apparelPiece =>
                    new Loot<ApparelPieceData>(
                        apparelPiece,
                        onTake: () =>
                        {
                            _apparelPieces.Remove(apparelPiece);
                            StateChanged?.Invoke();
                        }))
                .ToArray();

        public LootCollectionSource Clone() => new LootCollectionSource(_items, _apparelPieces);

        public void Merge(LootCollectionSource source)
        {
            _items.AddRange(source._items);
            _apparelPieces.AddRange(source._apparelPieces);
            StateChanged?.Invoke();
        }
    }
}