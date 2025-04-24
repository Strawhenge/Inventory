namespace Strawhenge.Inventory.Apparel
{
    public interface IApparelViewFactory
    {
        IApparelView Create(ApparelPieceData data);
    }
}