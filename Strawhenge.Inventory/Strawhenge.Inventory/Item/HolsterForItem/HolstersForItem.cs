using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Strawhenge.Common.Logging;

namespace Strawhenge.Inventory.Items.HolsterForItem
{
    public class HolstersForItem : IHolstersForItem
    {
        readonly IEnumerable<IHolsterForItem> _inner;
        readonly ILogger _logger;

        public HolstersForItem(IEnumerable<IHolsterForItem> inner, ILogger logger)
        {
            _inner = inner.ToArray();
            _logger = logger;
        }

        public bool IsEquippedToHolster(out IHolsterForItem holsterItem)
        {
            var equipped = _inner.Where(x => x.IsEquipped).ToArray();

            if (equipped.Length > 1)
                _logger.LogError("Item equipped to more than one holster.");

            holsterItem = equipped.FirstOrDefault();
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