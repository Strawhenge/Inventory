using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Unity.Items;
using System.Collections.Generic;

namespace Strawhenge.Inventory.Unity.Components
{
    public class HolsterScriptsContainer
    {
        readonly List<HolsterScript> _holsterScripts = new();
        readonly Holsters _holsters;

        public HolsterScriptsContainer(Holsters holsters)
        {
            _holsters = holsters;
        }

        public IEnumerable<HolsterScript> HolsterScripts => _holsterScripts;

        public void Add(HolsterScript holster)
        {
            _holsterScripts.Add(holster);
            _holsters.Add(holster.Name);
        }
    }
}