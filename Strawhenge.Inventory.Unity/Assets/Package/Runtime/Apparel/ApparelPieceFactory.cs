using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Effects;
using System.Linq;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public class ApparelPieceFactory
    {
        readonly ApparelSlot _defaultSlot = new ApparelSlot("Default");
        readonly ApparelSlotScripts _apparelSlotScripts;
        readonly IApparelGameObjectInitializer _gameObjectInitializer;
        readonly IApparelLayerAccessor _layerAccessor;
        readonly IApparelDrop _apparelDrop;
        readonly EffectFactory _effectFactory;
        readonly ILogger _logger;

        public ApparelPieceFactory(
            ApparelSlotScripts apparelSlotScripts,
            IApparelGameObjectInitializer gameObjectInitializer,
            IApparelLayerAccessor layerAccessor,
            IApparelDrop apparelDrop,
            EffectFactory effectFactory,
            ILogger logger)
        {
            _apparelSlotScripts = apparelSlotScripts;
            _gameObjectInitializer = gameObjectInitializer;
            _layerAccessor = layerAccessor;
            _apparelDrop = apparelDrop;
            _effectFactory = effectFactory;
            _logger = logger;
        }

        public ApparelPiece Create(IApparelPieceData source)
        {
            if (!_apparelSlotScripts.FindByName(source.Slot).HasSome(out var slotScript))
            {
                _logger.LogWarning($"Missing apparel slot: '{source.Slot}'.");
                return new ApparelPiece(source.Name, _defaultSlot, new NullApparelView());
            }

            var view = new ApparelView(
                source,
                _gameObjectInitializer,
                _layerAccessor,
                _apparelDrop,
                slotScript.transform);

            var effects = source.Effects.Select(_effectFactory.Create);

            return new ApparelPiece(source.Name, slotScript.ApparelSlot, view, effects);
        }
    }
}