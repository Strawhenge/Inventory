using Strawhenge.Inventory.Containers;
using System.Collections.Generic;
using UnityEngine;
using ILogger = Strawhenge.Common.Logging.ILogger;

namespace Strawhenge.Inventory.Unity.Components
{
    public class HolsterComponents
    {
        readonly List<IHolsterComponent> _components = new List<IHolsterComponent>();
        readonly Holsters _holsters;
        readonly ILogger _logger;

        public HolsterComponents(Holsters holsters, ILogger logger)
        {
            _holsters = holsters;
            _logger = logger;
        }

        public IEnumerable<IHolsterComponent> Components => _components;

        public void Add(string name, Transform transform)
        {
            _components.Add(
                new HolsterComponent(name, transform, _logger));

            _holsters.Add(name);
        }
    }
}