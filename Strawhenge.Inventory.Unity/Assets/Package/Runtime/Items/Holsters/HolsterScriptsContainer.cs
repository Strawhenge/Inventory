using FunctionalUtilities;
using System.Collections.Generic;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items
{
    public class HolsterScriptsContainer
    {
        readonly Dictionary<string, HolsterScript> _holstersByName = new();

        public Maybe<HolsterScript> this[string holsterName] => _holstersByName.MaybeGetValue(holsterName);

        public void Add(HolsterScript holster)
        {
            if (!_holstersByName.TryAdd(holster.HolsterName, holster))
                Debug.LogWarning($"Duplicate holster '{holster.HolsterName}'.");
        }
    }
}