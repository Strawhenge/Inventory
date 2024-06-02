using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Strawhenge.Inventory.Items.HolsterForItem
{
    public class HolstersForItem : IHolstersForItem
    {
        public static HolstersForItem None { get; } = new HolstersForItem(Array.Empty<IHolsterForItem>());

        readonly IEnumerable<IHolsterForItem> _inner;

        public HolstersForItem(IEnumerable<IHolsterForItem> inner)
        {
            _inner = inner.ToArray();
        }

        public bool IsEquippedToHolster(out IHolsterForItem holsterItem)
        {
            holsterItem = _inner.FirstOrDefault(x => x.IsEquipped);
            return holsterItem != null;
        }

        public bool IsEquippedToHolster(out IHolsterForItemView holsterItemView)
        {
            var isEquipped = IsEquippedToHolster(out IHolsterForItem holsterItem);
            holsterItemView = holsterItem?.GetView();
            return isEquipped;
        }

        IEnumerator<IHolsterForItem> IEnumerable<IHolsterForItem>.GetEnumerator() => _inner.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => (_inner as IEnumerable).GetEnumerator();
    }
}