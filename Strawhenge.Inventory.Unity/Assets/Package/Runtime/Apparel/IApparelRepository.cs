using FunctionalUtilities;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public interface IApparelRepository
    {
        Maybe<IApparelPieceData> FindByName(string name);
    }
}