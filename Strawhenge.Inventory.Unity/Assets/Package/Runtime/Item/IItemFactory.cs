using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Unity.Items.Context;
using Strawhenge.Inventory.Unity.Items.Data;

namespace Strawhenge.Inventory.Unity.Items
{
    public interface IItemFactory
    {
        Item Create(IItemData data);

        Item Create(IItemData data, ItemContext context);
    }
}