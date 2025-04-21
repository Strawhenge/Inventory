using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Items.Holsters;
using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.Data;
using Strawhenge.Inventory.Unity.Procedures.DropItem;
using Strawhenge.Inventory.Unity.Procedures.Holster;

namespace Strawhenge.Inventory.Unity.Procedures
{
    class HolsterForItemProcedures : IHolsterForItemProcedures
    {
        readonly ItemHelper _item;
        readonly IHolsterItemData _data;
        readonly HandScriptsContainer _handScripts;
        readonly HolsterScript _holster;
        readonly IProduceItemAnimationHandler _produceItemAnimationHandler;

        public HolsterForItemProcedures(
            ItemHelper item,
            HandScriptsContainer handScripts,
            HolsterScript holster,
            IProduceItemAnimationHandler produceItemAnimationHandler,
            ILogger logger)
        {
            _item = item;
            _data = item.GetHolsterData(holster, logger);
            _handScripts = handScripts;
            _holster = holster;
            _produceItemAnimationHandler = produceItemAnimationHandler;
        }

        public Procedure DrawLeftHand() => Draw(_handScripts.Left, _data.DrawFromHolsterLeftHandId);

        public Procedure DrawRightHand() => Draw(_handScripts.Right, _data.DrawFromHolsterRightHandId);

        Procedure Draw(HandScript hand, int id)
        {
            if (id == 0)
                return new SimpleDrawFromHolster(_holster, hand);

            return new AnimatedDrawFromHolster(_produceItemAnimationHandler, _holster, hand, id);
        }

        public Procedure PutAwayLeftHand() => PutAway(_handScripts.Left, _data.PutInHolsterLeftHandId);

        public Procedure PutAwayRightHand() => PutAway(_handScripts.Right, _data.PutInHolsterRightHandId);

        Procedure PutAway(HandScript hand, int id)
        {
            if (id == 0)
                return new SimplePutInHolster(hand, _holster);

            return new AnimatedPutInHolster(_produceItemAnimationHandler, hand, _holster, id);
        }

        public Procedure Show() => new ShowInHolster(_item, _holster);

        public Procedure Hide() => new HideInHolster(_holster);

        public Procedure Drop() => new SimpleDropFromHolster(_holster);
    }
}