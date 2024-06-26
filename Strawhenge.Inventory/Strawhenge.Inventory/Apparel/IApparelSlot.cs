using System;
using FunctionalUtilities;

namespace Strawhenge.Inventory.Apparel
{
    public interface IApparelSlot
    {
        event Action Changed;

        string Name { get; }

        Maybe<IApparelPiece> CurrentPiece { get; }
    }
}