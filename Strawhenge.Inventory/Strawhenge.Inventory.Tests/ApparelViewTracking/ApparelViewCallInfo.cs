namespace Strawhenge.Inventory.Tests
{
    class ApparelViewCallInfo
    {
        public static implicit operator ApparelViewCallInfo(
            (string apparelPieceName, string actionName) tuple) =>
            new ApparelViewCallInfo(tuple.apparelPieceName, tuple.actionName);

        public ApparelViewCallInfo(string apparelPieceName, string actionName)
        {
            ApparelPieceName = apparelPieceName;
            ActionName = actionName;
        }

        public string ApparelPieceName { get; }

        public string ActionName { get; }

        public override string ToString()
        {
            return $"{ApparelPieceName} => {ActionName}";
        }
    }
}