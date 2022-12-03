using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Common.Logging;

namespace Strawhenge.Inventory.Containers
{
    public class Holsters : IHolsters
    {
        readonly Dictionary<string, IHolster> _holsters = new Dictionary<string, IHolster>();
        readonly ILogger _logger;

        public Holsters(ILogger logger)
        {
            _logger = logger;
        }

        public void Add(string name)
        {
            if (_holsters.ContainsKey(name))
            {
                _logger.LogWarning($"Holster '{name}' already exists.");
                return;
            }

            _holsters.Add(name, new Holster(name));
        }

        public Maybe<IHolster> FindByName(string name)
        {
            if (_holsters.TryGetValue(name, out var holster))
            {
                return Maybe.Some(holster);
            }

            return Maybe.None<IHolster>();
        }

        public IEnumerable<IHolster> GetAll()
        {
            return _holsters.Values;
        }
    }
}