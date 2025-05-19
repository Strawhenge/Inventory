using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items.Consumables;
using Strawhenge.Inventory.Items.Holsters;

namespace Strawhenge.Inventory.Items
{
    public class ItemProcedureDto
    {
        readonly IDictionary<string, IItemHolsterProcedures> _holsterProcedures =
            new Dictionary<string, IItemHolsterProcedures>();

        public ItemProcedureDto(IItemProcedures itemProcedures)
        {
            ItemProcedures = itemProcedures;
        }

        public IItemProcedures ItemProcedures { get; set; }

        public Maybe<IConsumableProcedures> ConsumableProcedures { get; private set; }

        public Maybe<IItemHolsterProcedures> HolsterProcedures(string holsterName) =>
            _holsterProcedures.MaybeGetValue(holsterName);

        public void SetHolster(string holsterName, IItemHolsterProcedures procedures) =>
            _holsterProcedures[holsterName] = procedures;

        public void SetConsumable(IConsumableProcedures consumableProcedures) =>
            ConsumableProcedures = Maybe.Some(consumableProcedures);
    }
}