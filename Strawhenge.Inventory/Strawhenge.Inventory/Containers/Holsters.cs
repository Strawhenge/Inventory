using System.Collections;
using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Common.Logging;

namespace Strawhenge.Inventory.Containers
{
    public class Holsters : IEnumerable<ItemContainer>
    {
        readonly Dictionary<string, ItemContainer> _holsters = new Dictionary<string, ItemContainer>();
        readonly ILogger _logger;

        public Holsters(ILogger logger)
        {
            _logger = logger;
        }

        public Maybe<ItemContainer> this[string name] => _holsters.MaybeGetValue(name);

        public void Add(string name)
        {
            if (_holsters.ContainsKey(name))
            {
                _logger.LogWarning($"Holster '{name}' already exists.");
                return;
            }

            _holsters.Add(name, new ItemContainer(name));
        }

        public IEnumerator<ItemContainer> GetEnumerator() => _holsters.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _holsters.Values.GetEnumerator();
    }
}