using Strawhenge.Inventory.Unity.Data;

namespace Strawhenge.Inventory.Unity.Items
{
    public interface IItemFactory
    {
        IItem Create(IItemData data);
    }
}