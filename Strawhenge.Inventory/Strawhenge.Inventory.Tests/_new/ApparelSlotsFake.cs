using System.Collections.Generic;
using Strawhenge.Inventory.Apparel;

namespace Strawhenge.Inventory.Tests._new
{
    public class ApparelSlotsFake : IApparelSlots
    {
        readonly List<ApparelSlot> _apparelSlots = new List<ApparelSlot>();

        public IEnumerable<IApparelSlot> All => _apparelSlots;

        public void Add(string name) => _apparelSlots.Add(new ApparelSlot(name));
    }
}