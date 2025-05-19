namespace Strawhenge.Inventory.Apparel
{
    public interface IApparelViewFactory
    {
        IApparelView Create(ApparelPiece apparelPiece);
    }
}