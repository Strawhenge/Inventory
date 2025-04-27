using FunctionalUtilities;

namespace Strawhenge.Inventory.Apparel
{
    public interface IApparelRepository
    {
        Maybe<ApparelPieceData> FindByName(string name);
    }
}