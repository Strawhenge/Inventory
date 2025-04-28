using Strawhenge.Inventory.Unity.Items;
using System.Collections.Generic;

namespace Strawhenge.Inventory.Unity.Components
{
    public class HolsterScriptsContainer
    {
        readonly List<HolsterScript> _holsterScripts = new();

        public IEnumerable<HolsterScript> HolsterScripts => _holsterScripts;

        public void Add(HolsterScript holster)
        {
            _holsterScripts.Add(holster);
        }
    }
}