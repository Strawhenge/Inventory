using Strawhenge.Inventory.Containers;
using System.Collections.Generic;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Components
{
    public class HolsterComponents
    {
        readonly List<IHolsterComponent> components = new List<IHolsterComponent>();
        readonly IHolsters holsters;
        readonly ILogger logger;

        public HolsterComponents(IHolsters holsters, ILogger logger)
        {
            this.holsters = holsters;
            this.logger = logger;
        }

        public IEnumerable<IHolsterComponent> Components => components;

        public void Add(string name, Transform transform)
        {
            components.Add(
                new HolsterComponent(name, transform, logger));

            holsters.Add(name);
        }
    }
}