namespace Strawhenge.Inventory.Apparel
{
    public interface IApparelPieceFactory<TApparelSource>
    {
        ApparelPiece Create(TApparelSource source);
    }
}