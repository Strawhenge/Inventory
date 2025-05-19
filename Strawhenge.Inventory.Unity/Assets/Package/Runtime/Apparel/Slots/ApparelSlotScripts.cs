using FunctionalUtilities;
using Strawhenge.Common.Logging;
using System.Collections.Generic;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public class ApparelSlotScripts
    {
        readonly Dictionary<string, ApparelSlotScript> _slotsByName = new Dictionary<string, ApparelSlotScript>();
        readonly ILogger _logger;

        public ApparelSlotScripts(ILogger logger)
        {
            _logger = logger;
        }

        internal void Add(ApparelSlotScript apparelSlotScript)
        {
            if (_slotsByName.ContainsKey(apparelSlotScript.SlotName))
            {
                _logger.LogWarning($"Duplicate apparel slot: '{apparelSlotScript.SlotName}'.");
                return;
            }
            
            _slotsByName.Add(apparelSlotScript.SlotName, apparelSlotScript);
        }

        internal Maybe<ApparelSlotScript> FindByName(string name) =>
            _slotsByName.TryGetValue(name, out var script)
                ? Maybe.Some(script)
                : Maybe.None<ApparelSlotScript>();
    }
}