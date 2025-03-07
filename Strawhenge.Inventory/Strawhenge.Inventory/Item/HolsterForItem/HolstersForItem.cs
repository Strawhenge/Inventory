using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FunctionalUtilities;

namespace Strawhenge.Inventory.Items.Holsters
{
    public class HolstersForItem : IEnumerable<HolsterForItem>
    {
        public static HolstersForItem None { get; } = new HolstersForItem(Array.Empty<HolsterForItem>());

        readonly IReadOnlyDictionary<string, HolsterForItem> _holsters;

        public HolstersForItem(IEnumerable<HolsterForItem> inner)
        {
            _holsters = inner
                .ToDictionary(x => x.HolsterName, x => x);
        }

        public Maybe<HolsterForItem> this[string name] => _holsters.TryGetValue(name, out var holster)
            ? Maybe.Some(holster)
            : Maybe.None<HolsterForItem>();

        public bool IsEquippedToHolster(out HolsterForItem holsterItem)
        {
            holsterItem = _holsters.Values.FirstOrDefault(x => x.IsEquipped);
            return holsterItem != null;
        }

        internal bool IsEquippedToHolster(out IHolsterForItemView holsterItemView)
        {
            var isEquipped = IsEquippedToHolster(out HolsterForItem holsterItem);
            holsterItemView = holsterItem?.GetView();
            return isEquipped;
        }

        IEnumerator<HolsterForItem> IEnumerable<HolsterForItem>.GetEnumerator() => _holsters.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => (_holsters.Values as IEnumerable).GetEnumerator();
    }
}