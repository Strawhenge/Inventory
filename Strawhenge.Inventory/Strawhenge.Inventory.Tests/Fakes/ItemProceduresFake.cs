using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Procedures;

namespace Strawhenge.Inventory.Tests
{
    class ItemProceduresFake : IItemProcedures
    {
        readonly ProcedureTracker _procedureTracker;
        readonly string _itemName;

        public ItemProceduresFake(ProcedureTracker procedureTracker, string itemName)
        {
            _procedureTracker = procedureTracker;
            _itemName = itemName;
        }

        public Procedure AppearLeftHand() => CreateProcedure(nameof(AppearLeftHand));

        public Procedure AppearRightHand() => CreateProcedure(nameof(AppearRightHand));

        public Procedure DrawLeftHand() => CreateProcedure(nameof(DrawLeftHand));

        public Procedure DrawRightHand() => CreateProcedure(nameof(DrawRightHand));

        public Procedure PutAwayLeftHand() => CreateProcedure(nameof(PutAwayLeftHand));

        public Procedure PutAwayRightHand() => CreateProcedure(nameof(PutAwayRightHand));

        public Procedure DropLeftHand() => CreateProcedure(nameof(DropLeftHand));

        public Procedure DropRightHand() => CreateProcedure(nameof(DropRightHand));

        public Procedure SpawnAndDrop() => CreateProcedure(nameof(SpawnAndDrop));

        public Procedure LeftHandToRightHand() => CreateProcedure(nameof(LeftHandToRightHand));

        public Procedure RightHandToLeftHand() => CreateProcedure(nameof(RightHandToLeftHand));

        public Procedure DisappearLeftHand() => CreateProcedure(nameof(DisappearLeftHand));

        public Procedure DisappearRightHand() => CreateProcedure(nameof(DisappearRightHand));

        Procedure CreateProcedure(string procedureName)
        {
            var procedure = new TrackableProcedure(_itemName, procedureName);
            _procedureTracker.Track(procedure);
            return procedure;
        }
    }
}