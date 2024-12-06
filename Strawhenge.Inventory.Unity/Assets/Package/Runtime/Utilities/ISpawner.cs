using Strawhenge.Inventory.Unity.Data;
using Strawhenge.Inventory.Unity.Items;

namespace Strawhenge.Inventory.Unity
{
    public interface ISpawner
    {
        ItemScript Spawn(IItemData item);

        void Despawn(ItemScript script);
    }
}
