using Strawhenge.Common.Factories;
using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Procedures;

namespace Strawhenge.Inventory
{
    class InventoryContext
    {
        public InventoryContext(IAbstractFactory abstractFactory, ILogger logger)
        {
            AbstractFactory = abstractFactory;
            Hands = new Hands();
            Holsters = new Holsters(logger);
            StoredItems = new StoredItems(logger);
            ProcedureQueue = new ProcedureQueue();
        }

        public IAbstractFactory AbstractFactory { get; }

        public Hands Hands { get; }

        public Holsters Holsters { get; }

        public StoredItems StoredItems { get; }

        public ProcedureQueue ProcedureQueue { get; }
    }
}