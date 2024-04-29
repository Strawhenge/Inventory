using FunctionalUtilities;

namespace Strawhenge.Inventory.Apparel
{
    public class ApparelSlot : IApparelSlot
    {
        public ApparelSlot(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public Maybe<IApparelPiece> CurrentPiece { get; private set; } = Maybe.None<IApparelPiece>();

        internal void Set(IApparelPiece piece)
        {
            CurrentPiece.Do(x => x.Unequip());
            CurrentPiece = Maybe.Some(piece);
        }

        internal void Unset()
        {
            CurrentPiece = Maybe.None<IApparelPiece>();
        }
    }
}