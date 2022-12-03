using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Common.Logging;

namespace Strawhenge.Inventory.Containers
{
    public class Holsters : IHolsters
    {
        readonly Dictionary<string, IHolster> holsters = new Dictionary<string, IHolster>();
        readonly ILogger logger;

        public Holsters(ILogger logger)
        {
            this.logger = logger;
        }

        public void Add(string name)
        {
            if (holsters.ContainsKey(name))
            {
                logger.LogWarning($"Holster '{name}' already exists.");
                return;
            }

            holsters.Add(name, new Holster(name));
        }

        public Maybe<IHolster> FindByName(string name)
        {
            if (holsters.TryGetValue(name, out var holster))
            {
                return Maybe.Some(holster);
            }

            return Maybe.None<IHolster>();
        }

        public IEnumerable<IHolster> GetAll()
        {
            return holsters.Values;
        }
    }
}
