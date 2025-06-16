using FunctionalUtilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items
{
    public class HolsterScriptsContainer
    {
        readonly Dictionary<string, HolsterScript> _holstersByName;

        public HolsterScriptsContainer(IEnumerable<HolsterScript> holsters)
        {
            _holstersByName = new Dictionary<string, HolsterScript>();

            foreach (var holster in holsters)
            {
                if (_holstersByName.ContainsKey(holster.HolsterName))
                    Debug.LogWarning($"Duplicate holster '{holster.HolsterName}'.", holster);

                _holstersByName[holster.HolsterName] = holster;
            }
        }

        public IReadOnlyList<string> HolsterNames => _holstersByName.Keys.ToArray();

        public Maybe<HolsterScript> this[string holsterName] => _holstersByName.MaybeGetValue(holsterName);
    }
}