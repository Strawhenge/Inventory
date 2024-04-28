using System.Collections.Generic;

namespace Strawhenge.Inventory.Apparel
{
    public interface IApparelSlots
    {
        IEnumerable<IApparelSlot> All { get; }
    }
}