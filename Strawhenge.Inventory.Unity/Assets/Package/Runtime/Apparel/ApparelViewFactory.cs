using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Loot;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public class ApparelViewFactory : IApparelViewFactory
    {
        readonly ApparelSlotScriptsContainer _slotScripts;
        readonly LootDrop _apparelDrop;
        readonly ILogger _logger;

        public ApparelViewFactory(
            ApparelSlotScriptsContainer slotScripts,
            LootDrop apparelDrop,
            ILogger logger)
        {
            _slotScripts = slotScripts;
            _apparelDrop = apparelDrop;
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
                _apparelDrop,
                slotScript.transform);

            return view;
        }
    }
}