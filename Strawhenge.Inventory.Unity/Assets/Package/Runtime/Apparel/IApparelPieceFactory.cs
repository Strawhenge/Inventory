using Strawhenge.Inventory.Apparel;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public interface IApparelPieceFactory
    {
        IApparelPiece Create(IApparelPieceData source);
    }
}