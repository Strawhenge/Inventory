using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Common.Logging;

namespace Strawhenge.Inventory.Containers
{
    public class Holsters
    {
        readonly Dictionary<string, ItemContainer> _holsters = new Dictionary<string, ItemContainer>();
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

            _holsters.Add(name, new ItemContainer(name));
        }

        public Maybe<ItemContainer> FindByName(string name)
        {
            return _holsters.MaybeGetValue(name);
        }

        public IEnumerable<ItemContainer> GetAll()
        {
            return _holsters.Values;
        }
    }
}