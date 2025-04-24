using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Apparel;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public class ApparelViewFactory : IApparelViewFactory
    {
        readonly ApparelSlotScripts _slotScripts;
        readonly IApparelGameObjectInitializer _gameObjectInitializer;
        readonly IApparelDrop _apparelDrop;
        readonly IApparelLayerAccessor _layerAccessor;
        readonly ILogger _logger;

        public ApparelViewFactory(
            ApparelSlotScripts slotScripts,
            IApparelGameObjectInitializer gameObjectInitializer,
            IApparelDrop apparelDrop,
            IApparelLayerAccessor layerAccessor,
            ILogger logger)
        {
            _slotScripts = slotScripts;
            _gameObjectInitializer = gameObjectInitializer;
            _apparelDrop = apparelDrop;
            _layerAccessor = layerAccessor;
            _logger = logger;
        }

        public IApparelView Create(ApparelPieceData data)
        {
            if (!data.Get<IApparelPieceData>().HasSome(out var apparelPieceData))
            {
                _logger.LogWarning($"Missing apparel data.");
                return new NullApparelView();
            }


            if (!_slotScripts.FindByName(apparelPieceData.Slot).HasSome(out var slotScript))
            {
                _logger.LogWarning($"Missing apparel slot: '{apparelPieceData.Slot}'.");
                return new NullApparelView();
            }

            var view = new ApparelView(
                apparelPieceData,
                _gameObjectInitializer,
                _layerAccessor,
                _apparelDrop,
                slotScript.transform);

            return view;
        }
    }
}