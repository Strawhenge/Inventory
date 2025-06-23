using FunctionalUtilities;

namespace Strawhenge.Inventory.Apparel
{
    public interface IApparelPieceRepository
    {
        Maybe<ApparelPiece> FindByName(string name);
    }
}