using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FunctionalUtilities;
using Strawhenge.Common.Logging;

namespace Strawhenge.Inventory.Items
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

        public IEnumerable<InventoryItem> Items => _holsters.Values
            .SelectMany(holster => holster.CurrentItem.AsEnumerable());

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