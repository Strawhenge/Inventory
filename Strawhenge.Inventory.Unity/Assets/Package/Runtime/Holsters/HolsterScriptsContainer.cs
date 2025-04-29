using FunctionalUtilities;
using Strawhenge.Inventory.Unity.Items;
using System.Collections.Generic;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Components
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