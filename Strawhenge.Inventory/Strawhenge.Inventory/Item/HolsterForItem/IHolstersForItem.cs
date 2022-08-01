using System.Collections.Generic;

namespace Strawhenge.Inventory.Items.HolsterForItem
{
    public interface IHolstersForItem : IEnumerable<IHolsterForItem>
    {
        bool IsEquippedToHolster(out IHolsterForItem holsterItem);

        bool IsEquippedToHolster(out IHolsterForItemView holsterItemView);
    }
}
