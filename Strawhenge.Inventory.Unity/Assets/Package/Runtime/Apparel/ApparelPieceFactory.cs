using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Apparel;
using System.Linq;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public class ApparelPieceFactory : IApparelPieceFactory
    {
        readonly ApparelSlot _defaultSlot = new ApparelSlot("Default");
        readonly ApparelSlotScripts _apparelSlotScripts;
        readonly IApparelGameObjectInitializer _gameObjectInitializer;
        readonly IApparelLayerAccessor _layerAccessor;
        readonly ILogger _logger;

        public ApparelPieceFactory(
            ApparelSlotScripts apparelSlotScripts,
            IApparelGameObjectInitializer gameObjectInitializer,
            IApparelLayerAccessor layerAccessor,
            ILogger logger)
        {
            _apparelSlotScripts = apparelSlotScripts;
            _gameObjectInitializer = gameObjectInitializer;
            _layerAccessor = layerAccessor;
            _logger = logger;
        }

        public IApparelPiece Create(IApparelPieceData source)
        {
            if (!_apparelSlotScripts.FindByName(source.Slot).HasSome(out var slotScript))
            {
                _logger.LogWarning($"Missing apparel slot: '{source.Slot}'.");
                return new ApparelPiece(source.Name, _defaultSlot, new NullApparelView());
            }

            var view = new ApparelView(
                _gameObjectInitializer,
                _layerAccessor,
                source.Prefab,
                slotScript.transform,
                source.Position,
                source.Rotation,
                source.Scale);

            return new ApparelPiece(source.Name, slotScript.ApparelSlot, view);
        }
    }
}