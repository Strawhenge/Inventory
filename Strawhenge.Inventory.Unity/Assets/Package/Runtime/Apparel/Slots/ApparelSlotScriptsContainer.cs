using FunctionalUtilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public class ApparelSlotScriptsContainer
    {
        readonly Dictionary<string, ApparelSlotScript> _slotsByName;

        public ApparelSlotScriptsContainer(IEnumerable<ApparelSlotScript> apparelSlots)
        {
            _slotsByName = new Dictionary<string, ApparelSlotScript>();

            foreach (var apparelSlot in apparelSlots)
            {
                if (_slotsByName.ContainsKey(apparelSlot.SlotName))
                    Debug.LogWarning($"Duplicate apparel slot '{apparelSlot.SlotName}'.", apparelSlot);

                _slotsByName[apparelSlot.SlotName] = apparelSlot;
            }
        }

        internal IReadOnlyList<string> SlotNames => _slotsByName.Keys.ToArray();

        internal Maybe<ApparelSlotScript> FindByName(string name) =>
            _slotsByName.TryGetValue(name, out var script)
                ? Maybe.Some(script)
                : Maybe.None<ApparelSlotScript>();
    }
}