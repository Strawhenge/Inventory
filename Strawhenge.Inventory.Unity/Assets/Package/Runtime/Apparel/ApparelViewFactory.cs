using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Apparel;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public class ApparelViewFactory : IApparelViewFactory
    {
        readonly ApparelSlotScripts _slotScripts;
        readonly IApparelDrop _apparelDrop;
        readonly IApparelLayerAccessor _layerAccessor;
        readonly ILogger _logger;

        public ApparelViewFactory(
            ApparelSlotScripts slotScripts,
            IApparelDrop apparelDrop,
            IApparelLayerAccessor layerAccessor,
            ILogger logger)
        {
            _slotScripts = slotScripts;
            _apparelDrop = apparelDrop;
            _layerAccessor = layerAccessor;
            _logger = logger;
        }

        public IApparelView Create(ApparelPieceData data)
        {
            if (!_slotScripts.FindByName(data.Slot).HasSome(out var slotScript))
            {
                _logger.LogWarning($"Missing apparel slot: '{data.Slot}'.");
                return NullApparelView.Instance;
            }

            var view = new ApparelView(
                data,
                _layerAccessor,
                _apparelDrop,
                slotScript.transform);

            return view;
        }
    }
}