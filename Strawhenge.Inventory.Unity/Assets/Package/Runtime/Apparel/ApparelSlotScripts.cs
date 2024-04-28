using FunctionalUtilities;
using Strawhenge.Common;
using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Apparel;
using System.Collections.Generic;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public class ApparelSlotScripts : IApparelSlots
    {
        readonly Dictionary<string, ApparelSlotScript> _slotsByName = new Dictionary<string, ApparelSlotScript>();
        readonly ILogger _logger;

        public ApparelSlotScripts(ILogger logger)
        {
            _logger = logger;
        }

        public IEnumerable<IApparelSlot> All => _slotsByName.Values.ToArray(x => x.ApparelSlot);

        internal void Add(ApparelSlotScript apparelSlotScript)
        {
            if (_slotsByName.ContainsKey(apparelSlotScript.ApparelSlot.Name))
            {
                _logger.LogWarning($"Duplicate apparel slot: '{apparelSlotScript.ApparelSlot.Name}'.");
                return;
            }

            _slotsByName.Add(apparelSlotScript.ApparelSlot.Name, apparelSlotScript);
        }

        internal Maybe<ApparelSlotScript> FindByName(string name) =>
            _slotsByName.TryGetValue(name, out var script)
                ? Maybe.Some(script)
                : Maybe.None<ApparelSlotScript>();
    }
}