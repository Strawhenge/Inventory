using System;
using FunctionalUtilities;

namespace Strawhenge.Inventory.Apparel
{
    public class ApparelSlot : IApparelSlot
    {
        public ApparelSlot(string name)
        {
            Name = name;
        }

        public event Action Changed;

        public string Name { get; }

        public Maybe<ApparelPiece> CurrentPiece { get; private set; } = Maybe.None<ApparelPiece>();

        internal void Set(ApparelPiece piece)
        {
            CurrentPiece.Do(x => x.Unequip());
            CurrentPiece = Maybe.Some(piece);

            Changed?.Invoke();
        }

        internal void Unset()
        {
            CurrentPiece = Maybe.None<ApparelPiece>();
            Changed?.Invoke();
        }
    }
}