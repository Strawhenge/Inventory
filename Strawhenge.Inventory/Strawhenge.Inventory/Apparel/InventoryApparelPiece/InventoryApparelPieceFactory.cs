using System.Linq;
using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Effects;

namespace Strawhenge.Inventory.Apparel
{
    public class InventoryApparelPieceFactory
    {
        readonly ApparelSlot _missingApparelSlot = new ApparelSlot("Missing.");
        readonly ApparelSlots _slots;
        readonly EffectFactory _effectFactory;
        readonly IApparelViewFactory _apparelViewFactory;
        readonly ILogger _logger;

        internal InventoryApparelPieceFactory(
            ApparelSlots slots,
            EffectFactory effectFactory,
            IApparelViewFactory apparelViewFactory,
            ILogger logger)
        {
            _slots = slots;
            _effectFactory = effectFactory;
            _apparelViewFactory = apparelViewFactory;
            _logger = logger;
        }

        public InventoryApparelPiece Create(ApparelPiece apparelPiece)
        {
            var slot = _slots[apparelPiece.Slot]
                .Reduce(() =>
                {
                    _logger.LogError($"Missing apparel slot '{apparelPiece.Slot}'.");
                    return _missingApparelSlot;
                });

            var view = _apparelViewFactory.Create(apparelPiece);

            var effects = apparelPiece.Effects
                .Select(_effectFactory.Create);

            var inventoryApparelPiece = new InventoryApparelPiece(
                apparelPiece,
                slot,
                view,
                effects
            );

            return inventoryApparelPiece;
        }
    }
}