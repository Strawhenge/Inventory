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
            // TODO Handle duplicate names.
            _holstersByName = holsters.ToDictionary(x => x.HolsterName);
        }

        public IReadOnlyList<string> HolsterNames => _holstersByName.Keys.ToArray();

        public Maybe<HolsterScript> this[string holsterName] => _holstersByName.MaybeGetValue(holsterName);
    }
}