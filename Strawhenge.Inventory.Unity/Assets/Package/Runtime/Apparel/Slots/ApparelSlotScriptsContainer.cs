using FunctionalUtilities;
using Strawhenge.Common.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public class ApparelSlotScriptsContainer
    {
        readonly Dictionary<string, ApparelSlotScript> _slotsByName;

        public ApparelSlotScriptsContainer(IEnumerable<ApparelSlotScript> apparelSlotScripts)
        {
            // TODO Handle duplicate names.
            _slotsByName = apparelSlotScripts.ToDictionary(x => x.SlotName);
        }

        internal Maybe<ApparelSlotScript> FindByName(string name) =>
            _slotsByName.TryGetValue(name, out var script)
                ? Maybe.Some(script)
                : Maybe.None<ApparelSlotScript>();
    }
}