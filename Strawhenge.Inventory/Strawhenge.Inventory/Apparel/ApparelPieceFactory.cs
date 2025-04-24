using System.Linq;
using FunctionalUtilities;
using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Effects;

namespace Strawhenge.Inventory.Apparel
{
    public class ApparelPieceFactory
    {
        readonly ApparelSlot _missingApparelSlot = new ApparelSlot("Missing.");
        readonly IApparelSlots _slots;
        readonly EffectFactory _effectFactory;
        readonly IApparelViewFactory _apparelViewFactory;
        readonly ILogger _logger;

        public ApparelPieceFactory(
            IApparelSlots slots,
            EffectFactory effectFactory,
            IApparelViewFactory apparelViewFactory,
            ILogger logger)
        {
            _slots = slots;
            _effectFactory = effectFactory;
            _apparelViewFactory = apparelViewFactory;
            _logger = logger;
        }

        public ApparelPiece Create(ApparelPieceData data)
        {
            var slot = _slots.All
                .FirstOrNone(x => x.Name == data.Slot)
                .Reduce(() =>
                {
                    _logger.LogError($"Missing apparel slot '{data.Slot}'.");
                    return _missingApparelSlot;
                });

            var view = _apparelViewFactory.Create(data);

            var effects = data.Effects
                .Select(_effectFactory.Create);

            var apparelPiece = new ApparelPiece(
                data.Name,
                slot,
                view,
                effects
            );

            return apparelPiece;
        }
    }
}