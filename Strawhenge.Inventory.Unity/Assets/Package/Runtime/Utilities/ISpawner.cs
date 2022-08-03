using Strawhenge.Inventory.Unity.Data;
using Strawhenge.Inventory.Unity.Monobehaviours;

namespace Strawhenge.Inventory.Unity
{
    public interface ISpawner
    {
        ItemScript Spawn(IItemData item);

        void Despawn(ItemScript script);
    }
}
