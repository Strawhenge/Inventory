using System.Collections.Generic;
using FunctionalUtilities;

namespace Strawhenge.Inventory.Items.HolsterForItem
{
    public interface IHolstersForItem : IEnumerable<IHolsterForItem>
    {
        Maybe<IHolsterForItem> this[string name] { get; }

        bool IsEquippedToHolster(out IHolsterForItem holsterItem);

        bool IsEquippedToHolster(out IHolsterForItemView holsterItemView);
    }
}