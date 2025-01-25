using System;
using System.Collections.Generic;
using Strawhenge.Inventory.Apparel;

namespace Strawhenge.Inventory.Tests._new
{
    public class ApparelSlotsFake : IApparelSlots
    {
        public IEnumerable<IApparelSlot> All => Array.Empty<IApparelSlot>();
    }
}