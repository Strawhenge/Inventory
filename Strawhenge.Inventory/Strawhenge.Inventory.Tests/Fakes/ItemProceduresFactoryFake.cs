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

        public ItemProcedureDto Create(Item item, Context context)
        {
            var itemProcedures = new ItemProceduresFake(_tracker, item.Name);

            var dto = new ItemProcedureDto(itemProcedures);

            foreach (var itemHolster in item.Holsters)
            {
                var holsterProcedures =
                    new HolsterForItemProceduresFake(_tracker, item.Name, itemHolster.HolsterName);
                dto.SetHolster(itemHolster.HolsterName, holsterProcedures);
            }

            return dto;
        }
    }
}