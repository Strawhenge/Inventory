using Strawhenge.Common.Factories;
using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Effects;
using Strawhenge.Inventory.Procedures;

namespace Strawhenge.Inventory
{
    class InventoryContext
    {
        public InventoryContext(IEffectFactoryLocator effectFactoryLocator, ILogger logger)
        {
            EffectFactory = new EffectFactory(effectFactoryLocator, logger);
            Hands = new Hands();
            Holsters = new Holsters(logger);
            StoredItems = new StoredItems(logger);
            ApparelSlots = new ApparelSlots(logger);
            ProcedureQueue = new ProcedureQueue();
        }

        public Hands Hands { get; }

        public Holsters Holsters { get; }

        public StoredItems StoredItems { get; }

        public ApparelSlots ApparelSlots { get; }

        public ProcedureQueue ProcedureQueue { get; }

        public EffectFactory EffectFactory { get; }
    }
}