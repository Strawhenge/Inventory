namespace Strawhenge.Inventory.Apparel
{
    public interface IApparelPieceFactory<TApparelSource>
    {
        IApparelPiece Create(TApparelSource source);
    }
}