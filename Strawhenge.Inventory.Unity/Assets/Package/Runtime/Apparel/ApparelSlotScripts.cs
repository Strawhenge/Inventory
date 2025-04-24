using FunctionalUtilities;
using Strawhenge.Common;
using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Apparel;
using System.Collections.Generic;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public class ApparelSlotScripts
    {
        readonly Dictionary<string, ApparelSlotScript> _slotsByName = new Dictionary<string, ApparelSlotScript>();
        readonly ApparelSlots _apparelSlots;
        readonly ILogger _logger;

        public ApparelSlotScripts(ApparelSlots apparelSlots, ILogger logger)
        {
            _apparelSlots = apparelSlots;
            _logger = logger;
        }

        public IEnumerable<ApparelSlot> All => _slotsByName.Values.ToArray(x => x.ApparelSlot);

        internal void Add(ApparelSlotScript apparelSlotScript)
        {
            if (_slotsByName.ContainsKey(apparelSlotScript.ApparelSlot.Name))
            {
                _logger.LogWarning($"Duplicate apparel slot: '{apparelSlotScript.ApparelSlot.Name}'.");
                return;
            }

            _apparelSlots.Add(apparelSlotScript.ApparelSlot);
            _slotsByName.Add(apparelSlotScript.ApparelSlot.Name, apparelSlotScript);
        }

        internal Maybe<ApparelSlotScript> FindByName(string name) =>
            _slotsByName.TryGetValue(name, out var script)
                ? Maybe.Some(script)
                : Maybe.None<ApparelSlotScript>();
    }
}