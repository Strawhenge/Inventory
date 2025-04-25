using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory.Tests
{
    class ItemProceduresFactoryFake : IItemProceduresFactory
    {
        readonly ProcedureTracker _tracker;

        public ItemProceduresFactoryFake(ProcedureTracker tracker)
        {
            _tracker = tracker;
        }

        public ItemProcedureDto Create(ItemData itemData)
        {
            var itemProcedures = new ItemProceduresFake(_tracker, itemData.Name);

            var dto = new ItemProcedureDto(itemProcedures);

            foreach (var holsterData in itemData.Holsters)
            {
                var holsterProcedures =
                    new HolsterForItemProceduresFake(_tracker, itemData.Name, holsterData.HolsterName);
                dto.SetHolster(holsterData.HolsterName, holsterProcedures);
            }

            return dto;
        }
    }
}