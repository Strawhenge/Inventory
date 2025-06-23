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
        readonly List<Item> _items;
        readonly List<ApparelPiece> _apparelPieces;

        public LootCollectionSource(
            IEnumerable<Item> items = null,
            IEnumerable<ApparelPiece> apparelPieces = null)
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

        public void Add(Item item)
        {
            _items.Add(item);
            StateChanged?.Invoke();
        }

        public void Add(ApparelPiece apparelPiece)
        {
            _apparelPieces.Add(apparelPiece);
            StateChanged?.Invoke();
        }

        public IReadOnlyList<Loot<Item>> GetItems() =>
            _items
                .Select(item =>
                    new Loot<Item>(
                        item,
                        onTake: () =>
                        {
                            _items.Remove(item);
                            StateChanged?.Invoke();
                        }))
                .ToArray();

        public IReadOnlyList<Loot<ApparelPiece>> GetApparelPieces() =>
            _apparelPieces
                .Select(apparelPiece =>
                    new Loot<ApparelPiece>(
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