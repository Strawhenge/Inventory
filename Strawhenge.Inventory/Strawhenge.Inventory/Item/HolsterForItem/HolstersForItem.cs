using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FunctionalUtilities;

namespace Strawhenge.Inventory.Items.HolsterForItem
{
    public class HolstersForItem : IEnumerable<IHolsterForItem>
    {
        public static HolstersForItem None { get; } = new HolstersForItem(Array.Empty<IHolsterForItem>());

        readonly IReadOnlyDictionary<string, IHolsterForItem> _holsters;

        public HolstersForItem(IEnumerable<IHolsterForItem> inner)
        {
            _holsters = inner
                .ToDictionary(x => x.HolsterName, x => x);
        }

        public Maybe<IHolsterForItem> this[string name] => _holsters.TryGetValue(name, out var holster)
            ? Maybe.Some(holster)
            : Maybe.None<IHolsterForItem>();

        public bool IsEquippedToHolster(out IHolsterForItem holsterItem)
        {
            holsterItem = _holsters.Values.FirstOrDefault(x => x.IsEquipped);
            return holsterItem != null;
        }

        internal bool IsEquippedToHolster(out IHolsterForItemView holsterItemView)
        {
            var isEquipped = IsEquippedToHolster(out IHolsterForItem holsterItem);
            holsterItemView = holsterItem?.GetView();
            return isEquipped;
        }

        IEnumerator<IHolsterForItem> IEnumerable<IHolsterForItem>.GetEnumerator() => _holsters.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => (_holsters.Values as IEnumerable).GetEnumerator();
    }
}