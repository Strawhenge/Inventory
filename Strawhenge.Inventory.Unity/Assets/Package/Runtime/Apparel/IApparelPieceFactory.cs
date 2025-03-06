using Strawhenge.Inventory.Apparel;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public interface IApparelPieceFactory
    {
        ApparelPiece Create(IApparelPieceData source);
    }
}