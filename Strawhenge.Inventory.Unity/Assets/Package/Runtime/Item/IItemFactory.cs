using Strawhenge.Inventory.Unity.Data;
using Strawhenge.Inventory.Unity.Monobehaviours;

namespace Strawhenge.Inventory.Unity.Items
{
    public interface IItemFactory
    {
        IItem Create(ItemScript itemScript);
        
        IItem Create(IItemData data);
    }
}