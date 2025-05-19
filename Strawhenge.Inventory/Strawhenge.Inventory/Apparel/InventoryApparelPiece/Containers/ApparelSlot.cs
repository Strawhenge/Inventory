using System;
using FunctionalUtilities;
using Strawhenge.Inventory.Apparel;

namespace Strawhenge.Inventory.Apparel
{
    public class ApparelSlot
    {
        public ApparelSlot(string name)
        {
            Name = name;
        }

        public event Action Changed;

        public string Name { get; }

        public Maybe<InventoryApparelPiece> CurrentPiece { get; private set; } = Maybe.None<InventoryApparelPiece>();

        internal void Set(InventoryApparelPiece piece)
        {
            CurrentPiece.Do(x => x.Unequip());
            CurrentPiece = Maybe.Some(piece);

            Changed?.Invoke();
        }

        internal void Unset()
        {
            CurrentPiece = Maybe.None<InventoryApparelPiece>();
            Changed?.Invoke();
        }
    }
}