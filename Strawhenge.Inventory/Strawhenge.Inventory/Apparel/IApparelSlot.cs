using FunctionalUtilities;

namespace Strawhenge.Inventory.Apparel
{
    public interface IApparelSlot
    {
        string Name { get; }

        Maybe<IApparelPiece> CurrentPiece { get; }
    }
}