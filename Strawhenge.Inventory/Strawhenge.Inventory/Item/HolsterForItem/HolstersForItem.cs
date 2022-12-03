using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Strawhenge.Common.Logging;

namespace Strawhenge.Inventory.Items.HolsterForItem
{
    public class HolstersForItem : IHolstersForItem
    {
        readonly IEnumerable<IHolsterForItem> inner;
        readonly ILogger logger;

        public HolstersForItem(IEnumerable<IHolsterForItem> inner, ILogger logger)
        {
            this.inner = inner.ToArray();
            this.logger = logger;
        }

        public bool IsEquippedToHolster(out IHolsterForItem holsterItem)
        {
            var equipped = inner.Where(x => x.IsEquipped);

            if (equipped.Count() > 1)
                logger.LogError("Item equipped to more than one holster.");

            holsterItem = equipped.FirstOrDefault();
            return holsterItem != null;
        }

        public bool IsEquippedToHolster(out IHolsterForItemView holsterItemView)
        {
            var isEquipped = IsEquippedToHolster(out IHolsterForItem holsterItem);
            holsterItemView = holsterItem?.GetView();
            return isEquipped;
        }

        IEnumerator<IHolsterForItem> IEnumerable<IHolsterForItem>.GetEnumerator() => inner.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => (inner as IEnumerable).GetEnumerator();
    }
}
