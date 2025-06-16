using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Unity.Loot;

namespace Strawhenge.Inventory.Unity.Apparel
{
    class ApparelViewFactory : IApparelViewFactory
    {
        readonly ApparelSlotScriptsContainer _slotScripts;
        readonly LootDrop _lootDrop;
        readonly PrefabInstantiatedEvents _prefabInstantiatedEvents;
        readonly ILogger _logger;

        public ApparelViewFactory(
            ApparelSlotScriptsContainer slotScripts,
            LootDrop lootDrop,
            PrefabInstantiatedEvents prefabInstantiatedEvents,
            ILogger logger)
        {
            _slotScripts = slotScripts;
            _lootDrop = lootDrop;
            _prefabInstantiatedEvents = prefabInstantiatedEvents;
            _logger = logger;
        }

        public IApparelView Create(ApparelPiece data)
        {
            if (!_slotScripts.FindByName(data.Slot).HasSome(out var slotScript))
            {
                _logger.LogWarning($"Missing apparel slot: '{data.Slot}'.");
                return NullApparelView.Instance;
            }

            var view = new ApparelView(
                data,
                slotScript,
                _lootDrop,
                _prefabInstantiatedEvents);

            return view;
        }
    }
}