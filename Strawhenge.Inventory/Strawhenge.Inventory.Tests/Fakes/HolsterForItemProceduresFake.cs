using Strawhenge.Inventory.Items.Holsters;
using Strawhenge.Inventory.Procedures;

namespace Strawhenge.Inventory.Tests
{
    class HolsterForItemProceduresFake : IHolsterForItemProcedures
    {
        readonly ProcedureTracker _procedureTracker;
        readonly string _itemName;
        readonly string _holsterName;

        public HolsterForItemProceduresFake(ProcedureTracker procedureTracker, string itemName, string holsterName)
        {
            _procedureTracker = procedureTracker;
            _itemName = itemName;
            _holsterName = holsterName;
        }

        public Procedure DrawLeftHand() => CreateProcedure(nameof(DrawLeftHand));

        public Procedure DrawRightHand() => CreateProcedure(nameof(DrawRightHand));

        public Procedure PutAwayLeftHand() => CreateProcedure(nameof(PutAwayLeftHand));

        public Procedure PutAwayRightHand() => CreateProcedure(nameof(PutAwayRightHand));

        public Procedure Show() => CreateProcedure(nameof(Show));

        public Procedure Hide() => CreateProcedure(nameof(Hide));

        public Procedure Drop() => CreateProcedure(nameof(Drop));

        Procedure CreateProcedure(string procedureName)
        {
            var procedure = new TrackableProcedure(_itemName, _holsterName, procedureName);
            _procedureTracker.Track(procedure);
            return procedure;
        }
    }
}