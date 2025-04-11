using FunctionalUtilities;

namespace Strawhenge.Inventory.Tests
{
    public class ProcedureInfo
    {
        public static implicit operator ProcedureInfo(
            (string itemName, string procedureName) tuple) =>
            new ProcedureInfo(tuple.itemName, tuple.procedureName);

        public static implicit operator ProcedureInfo(
            (string itemName, string holsterName, string procedureName) tuple) =>
            new ProcedureInfo(tuple.itemName, tuple.holsterName, tuple.procedureName);

        public ProcedureInfo(string itemName, string procedureName)
        {
            ItemName = itemName;
            ProcedureName = procedureName;
        }

        public ProcedureInfo(string itemName, string holsterName, string procedureName)
        {
            ItemName = itemName;
            HolsterName = holsterName;
            ProcedureName = procedureName;
        }

        public string ItemName { get; }

        public Maybe<string> HolsterName { get; } = Maybe.None<string>();

        public string ProcedureName { get; }

        public override string ToString()
        {
            return HolsterName
                .Map(holsterName => $"{ItemName} => {holsterName} => {ProcedureName}")
                .Reduce(() => $"{ItemName} => {ProcedureName}");
        }
    }
}