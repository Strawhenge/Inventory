using System.Collections.Generic;

namespace Strawhenge.Inventory.Apparel
{
    public interface IApparelSlots
    {
        IEnumerable<ApparelSlot> All { get; }
    }
}