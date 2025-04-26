using FunctionalUtilities;
using Strawhenge.Inventory.Apparel;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public interface IApparelRepository
    {
        Maybe<ApparelPieceData> FindByName(string name);
    }
}