using Strawhenge.Inventory.Unity.Items.Data;

namespace Strawhenge.Inventory.Unity.Items
{
    public interface IItemFactory
    {
        IItem Create(IItemData data);
    }
}