namespace Strawhenge.Inventory.Apparel
{
    public class ApparelSlot
    {
        public ApparelSlot(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public Maybe<ApparelPiece> CurrentPiece { get; private set; } = Maybe.None<ApparelPiece>();

        internal void Set(ApparelPiece piece)
        {
            CurrentPiece.Do(x => x.Unequip());
            CurrentPiece = piece;
        }

        internal void Unset()
        {
            CurrentPiece = Maybe.None<ApparelPiece>();
        }
    }
}
