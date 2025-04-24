using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Common.Logging;

namespace Strawhenge.Inventory.Apparel
{
    public class ApparelSlots
    {
        readonly Dictionary<string, ApparelSlot> _slots = new Dictionary<string, ApparelSlot>();
        readonly ILogger _logger;

        public ApparelSlots(ILogger logger)
        {
            _logger = logger;
        }

        public IEnumerable<ApparelSlot> All => _slots.Values;

        public Maybe<ApparelSlot> this[string name] => _slots.MaybeGetValue(name);

        public void Add(string name)
        {
            if (_slots.ContainsKey(name))
            {
                _logger.LogError($"Apparel slot '{name}' already exists.");
                return;
            }

            _slots.Add(name, new ApparelSlot(name));
        }

        public void Add(ApparelSlot slot)
        {
            if (_slots.ContainsKey(slot.Name))
            {
                _logger.LogError($"Apparel slot '{slot.Name}' already exists.");
                return;
            }

            _slots.Add(slot.Name, slot);
        }
    }
}