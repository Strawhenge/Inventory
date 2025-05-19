using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory.Items
{
    public class InventoryItemHolsters : IEnumerable<InventoryItemHolster>
    {
        public static InventoryItemHolsters None { get; } =
            new InventoryItemHolsters(Array.Empty<InventoryItemHolster>());

        readonly Dictionary<string, InventoryItemHolster> _holsters;

        public InventoryItemHolsters(IEnumerable<InventoryItemHolster> inner)
        {
            _holsters = inner
                .ToDictionary(x => x.HolsterName, x => x);
        }

        public Maybe<InventoryItemHolster> this[string name] => _holsters.MaybeGetValue(name);

        public bool IsEquippedToHolster(out InventoryItemHolster holsterItem)
        {
            holsterItem = _holsters.Values.FirstOrDefault(x => x.IsEquipped);
            return holsterItem != null;
        }

        internal bool IsEquippedToHolster(out ItemHolsterProcedureScheduler procedureScheduler)
        {
            var isEquipped = IsEquippedToHolster(out InventoryItemHolster holsterItem);
            procedureScheduler = holsterItem?.GetProcedureScheduler();
            return isEquipped;
        }

        IEnumerator<InventoryItemHolster> IEnumerable<InventoryItemHolster>.GetEnumerator() =>
            _holsters.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => (_holsters.Values as IEnumerable).GetEnumerator();
    }
}