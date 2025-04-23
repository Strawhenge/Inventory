using Strawhenge.Common.Factories;
using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Effects;
using Strawhenge.Inventory.Procedures;

namespace Strawhenge.Inventory
{
    class InventoryContext
    {
        public InventoryContext(IAbstractFactory abstractFactory, ILogger logger)
        {
            EffectFactory = new EffectFactory(abstractFactory, logger);
            Hands = new Hands();
            Holsters = new Holsters(logger);
            StoredItems = new StoredItems(logger);
            ProcedureQueue = new ProcedureQueue();
        }

        public Hands Hands { get; }

        public Holsters Holsters { get; }

        public StoredItems StoredItems { get; }

        public ProcedureQueue ProcedureQueue { get; }

        public EffectFactory EffectFactory { get; }
    }
}