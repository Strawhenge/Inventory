using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Effects;

namespace Strawhenge.Inventory
{
    public class Inventory
    {
        readonly ApparelPieceFactory _apparelPieceFactory;

        public Inventory(
            StoredItems storedItems,
            Hands hands,
            Holsters holsters,
            ApparelSlots apparelSlots,
            IEffectFactoryLocator effectFactoryLocator,
            IApparelViewFactory apparelViewFactory,
            ILogger logger)
        {
            Hands = hands;
            Holsters = holsters;
            ApparelSlots = apparelSlots;
            StoredItems = storedItems;

            var effectFactory = new EffectFactory(effectFactoryLocator, logger);

            _apparelPieceFactory = new ApparelPieceFactory(
                apparelSlots,
                effectFactory,
                apparelViewFactory,
                logger);
        }

        public Hands Hands { get; }

        public Holsters Holsters { get; }

        public StoredItems StoredItems { get; }

        public ApparelSlots ApparelSlots { get; }

        public ApparelPiece CreateApparelPiece(ApparelPieceData data)
        {
            return _apparelPieceFactory.Create(data);
        }
    }
}